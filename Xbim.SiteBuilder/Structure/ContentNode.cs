using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xbim.SiteBuilder.Templates;

namespace Xbim.SiteBuilder.Structure
{
    public abstract class ContentNode
    {
        public string Content { get; protected set; } = "";
        public DirectoryInfo Directory { get; protected set; }
        public DirectoryInfo RootDirectory { get; protected set; }
        public abstract PageSettings Settings { get; }
        public string SourcePath { get; protected set; }
        public string RelativePath { get; protected set; }
        public string Title { get; protected set; }
        public string UrlName { get; protected set; }
        public List<ContentNode> Children { get; } = new List<ContentNode>();
        public ContentNode Parent { get; protected set; }

        public string RelativeUrl { get { return RelativePath.Replace(Path.DirectorySeparatorChar, '/'); } }
        public int Depth
        {
            get
            {
                if (string.IsNullOrWhiteSpace(RelativePath))
                    return 0;
                return RelativePath.Count(c => c == Path.DirectorySeparatorChar);
            }
        }

        public ContentNode RootNode { get
            {
                if (Parent == null)
                    return this;
                return Parent.RootNode;
            } }

        protected string GetRelativePath(string filespec, string folder)
        {
            Uri pathUri = new Uri(filespec);
            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folder += Path.DirectorySeparatorChar;
            }
            Uri folderUri = new Uri(folder);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }

        protected static IEnumerable<IPageTemplate> Templates { get; } 
            = typeof(IPageTemplate).Assembly.GetTypes()
            .Where(t => typeof(IPageTemplate).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
            .Select(t => Activator.CreateInstance(t) as IPageTemplate);

        public abstract void Render(DirectoryInfo webRoot);

        protected virtual PageSettings GetSettings(ref string content)
        {
            if (!content.StartsWith("<!--"))
                return null;

            var exp = new Regex("<!--(?<data>.*)-->", RegexOptions.Singleline);
            var match = exp.Match(content);
            var data = match?.Groups["data"]?.Value;
            if (string.IsNullOrWhiteSpace(data))
                return null;

            //remove settings from the data
            content = content.Replace(match.Value, "");

            //deserialize settings
            var reader = new StringReader(data);
            var json = new Newtonsoft.Json.JsonSerializer();
            var result = json.Deserialize(reader, typeof(PageSettings));
            return result as PageSettings;
        }

        protected string MakeExternalLinksOpenBlank(string content)
        {
            content = content.Replace("href=\"http", "target=\"_blank\" href=\"http");
            content = content.Replace("href='http", "target=\"_blank\" href='http");
            return content;
        }

        private static Regex _pathExp = new Regex("(href|src)=['|\"](?<path>.+?)['|\"]", RegexOptions.IgnoreCase);

        protected string MakeRelativePaths(string content, int depth, ContentNode root, ContentNode contentNode, DirectoryInfo rootDir)
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
                        Console.WriteLine($"{path} not found in {contentNode.Title} ({contentNode.RelativeUrl})");

                    //prefix with relative path
                    for (int i = 0; i < depth; i++)
                        rPath = "../" + rPath;

                    //replace in content
                    content = content.Replace(path, rPath);
                }
            }

            return content;
        }

        protected bool PathExists(string path, ContentNode node, DirectoryInfo rootDir = null)
        {
            //check physical file
            if (rootDir != null)
            {
                var filePath = Path.Combine(rootDir.FullName, path.Replace('/', Path.DirectorySeparatorChar));
                if (File.Exists(filePath))
                    return true;
            }

            if (node is DirectoryNode)
                return node.Children.Any(c => PathExists(path, c));

            if (path.Equals(node.RelativeUrl))
                return true;

            return false;
        }
    }


}
