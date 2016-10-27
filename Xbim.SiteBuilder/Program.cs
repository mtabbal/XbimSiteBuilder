using HeyRed.MarkdownSharp;
using Xbim.SiteBuilder.Templates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ColorCode;
using ColorCode.Formatting;
using ColorCode.Styling.StyleSheets;

namespace Xbim.SiteBuilder
{
    class Program
    {
        private static Markdown md = new Markdown();
        private static ColorCode.CodeColorizer cc = new ColorCode.CodeColorizer();
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No data directory specified.");
                return;
            }

            var workDir = new DirectoryInfo(args[0]);

            var root = Path.Combine(workDir.FullName, "root");
            if (args.Length > 1)
                root = args[1];
            var rootDir = new DirectoryInfo(root);

            var resDir = workDir.GetDirectories("Resources").FirstOrDefault();
            var dataDir = workDir.GetDirectories("Data").FirstOrDefault();



            //create or recreate output folder
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);


            //copy all directories and files from resources to target root
            DirectoryCopy(resDir.FullName, root);

            //Create code colouring CSS file
            var codeCss = (StyleSheets.Default as DefaultStyleSheet).GetCssFile();
            var codeCssPath = Path.Combine(root, "css", "codestyles.css");
            File.WriteAllText(codeCssPath, codeCss);

            //build content structure
            var contentRoot = new ContentNode(dataDir, dataDir);

            RenderContent(contentRoot.Children, rootDir, contentRoot, rootDir);

        }

        private static void RenderContent(IEnumerable<ContentNode> nodes, DirectoryInfo outDir, ContentNode contentRoot, DirectoryInfo rootDir)
        {
            foreach (var node in nodes)
            {
                if (node.ContentType == DataType.Folder)
                {
                    var subDir = outDir.CreateSubdirectory(node.UrlName);
                    RenderContent(node.Children, subDir, contentRoot, rootDir);
                    continue;
                }

                var data = File.ReadAllText(node.AbsolutePath, Encoding.UTF8);
                if (node.ContentType == DataType.Markdown)
                {
                    //transform tables which are not supported in this Markdown processor
                    data = TransformTables(data);
                    data = TransformCodeBlocks(data);

                    //transform markdown syntax
                    data = md.Transform(data);
                }

                var template = new Layout
                {
                    Content = data,
                    NavigationRoot = contentRoot,
                    WithBanner = node.RelativeUrl == "index.html"
                };
                var content = template.TransformText();

                //fix root absolute paths to relative path - levels up to get to root
                MakeRelativePaths(ref content, node.Depth, contentRoot, node, rootDir);

                //save the result relative to root directory
                var htmlFile = Path.Combine(outDir.FullName, node.UrlName);
                File.WriteAllText(htmlFile, content, Encoding.UTF8);
            }


        }

        private static string TransformTables(string content)
        {
            var reader = new StringReader(content);
            var writer = new StringWriter();

            //find tables
            var line = reader.ReadLine();
            var inTable = false;
            while (line != null)
            {
                if (!line.StartsWith("|"))
                {
                    inTable = false;
                    writer.WriteLine(line);
                    line = reader.ReadLine();
                    continue;
                }

                //check next line for context
                var nextLine = reader.ReadLine();

                //start table
                if (!inTable)
                {
                    writer.WriteLine("<table class=\"table\">");
                    inTable = true;

                    //line is a header
                    if (nextLine.StartsWith("|---"))
                    {
                        line = line.Trim('|', ' ');
                        writer.WriteLine("<thead>");
                        line = "<tr><th>" + line.Replace("|", "</th><th>") + "</th></tr>";
                        writer.WriteLine(line);
                        writer.WriteLine("</thead>");
                        writer.WriteLine("<tbody>");
                        line = reader.ReadLine();
                        continue;
                    }
                    else
                    {
                        writer.WriteLine("<tbody>");
                    }
                }


                //end of table
                if (nextLine == null || !nextLine.StartsWith("|"))
                {
                    writer.WriteLine("</tbody>");
                    writer.WriteLine("</table>");
                    line = nextLine;
                    continue;
                }

                //write normal table row
                line = line.Trim('|', ' ');
                line = "<tr><td>" + line.Replace("|", "</td><td>") + "</td></tr>";
                writer.WriteLine(line);
                line = nextLine;
            }

            return writer.ToString();

        }

        private static string TransformCodeBlocks(string content)
        {
            var reader = new StringReader(content);
            var writer = new StringWriter();
            var code = new StringWriter();
            var line = reader.ReadLine();
            var lang = "";
            var inCodeBlock = false;
            while (line != null)
            {
                if (!line.StartsWith("```"))
                {
                    if (inCodeBlock)
                        code.WriteLine(line);
                    else
                        writer.WriteLine(line);
                    line = reader.ReadLine();
                    continue;
                }

                if (!inCodeBlock)
                {
                    lang = line.Trim('`', ' ');
                    inCodeBlock = true;
                }
                else
                {
                    var codeContent = code.ToString();
                    if (lang.Equals("cs", StringComparison.InvariantCultureIgnoreCase))
                        cc.Colorize(codeContent, Languages.CSharp, new HtmlClassFormatter(), StyleSheets.Default, writer);
                    else if (lang.Equals("js", StringComparison.InvariantCultureIgnoreCase))
                        cc.Colorize(codeContent, Languages.JavaScript, new HtmlClassFormatter(), StyleSheets.Default, writer);
                    else if (lang.Equals("step", StringComparison.InvariantCultureIgnoreCase))
                        cc.Colorize(codeContent, Languages.Step, new HtmlClassFormatter(), StyleSheets.Default, writer);
                    else
                    {
                        codeContent = $"<pre><code>{codeContent}</code></pre>";
                        writer.Write(codeContent);
                    }


                    //clear
                    code = new StringWriter();
                    inCodeBlock = false;
                    lang = "";
                }

                line = reader.ReadLine();
            }
            return writer.ToString();
        }

        private static Regex _pathExp = new Regex("(href|src)=['|\"](?<path>.+?)['|\"]", RegexOptions.IgnoreCase );

        private static void MakeRelativePaths(ref string content, int depth, ContentNode root, ContentNode contentNode, DirectoryInfo rootDir)
        {
            //get all href and src attributes
            var srcs = _pathExp.Matches(content);

            var paths = new List<string>();

            foreach (Match src in srcs)
            {
                var path = src.Groups["path"].Value;
                paths.Add(path);

            }

            //make paths relative
            foreach (var path in paths.Distinct())
            {
                if (path.StartsWith("/") && !path.StartsWith("//"))
                {
                    //trim leading slash
                    var rPath = path.Substring(1);

                    //validate all paths (it can either be file resource or content node)
                    if (!PathExists(rPath, root, rootDir))
                        Console.WriteLine($"{path} not found in {contentNode.Name} ({contentNode.RelativeUrl})");

                    //prefix with relative path
                    for (int i = 0; i < depth; i++)
                        rPath = "../" + rPath;

                    //replace in content
                    content = content.Replace(path, rPath);
                }
            }

        }

        private static bool PathExists(string path, ContentNode node, DirectoryInfo rootDir = null)
        {
            //check physical file
            if (rootDir != null)
            {
                var filePath = Path.Combine(rootDir.FullName, path.Replace('/', Path.DirectorySeparatorChar));
                if (File.Exists(filePath))
                    return true;
            }

            if (node.ContentType == DataType.Folder)
            {
                return node.Children.Any(c => PathExists(path, c));
            }
            if (path.Equals(node.RelativeUrl))
                return true;
            return false;
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs = true)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

    }
}
