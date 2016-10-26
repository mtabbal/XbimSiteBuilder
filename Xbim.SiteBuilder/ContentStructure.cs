using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xbim.SiteBuilder
{
    public class ContentNode
    {
        private const string _mdExtension = ".md.txt";
        private const string _mdFilter = "*.md.txt";
        private const string _htmlExtension = ".html";
        private const string _htmlFilter = "*.html";
        public DirectoryInfo Dir { get; private set; }
        public DirectoryInfo Root { get; private set; }

        private ContentNode()
        {

        }

        public ContentNode(DirectoryInfo dataDir, DirectoryInfo root)
        {
            Dir = dataDir;
            Root = root;
            AbsolutePath = dataDir.FullName;
            RelativePath = GetRelativePath(AbsolutePath + Path.DirectorySeparatorChar, root.FullName).URLFriendly();
            Name = dataDir.Name;
            UrlName = dataDir.Name.URLFriendly();
            ContentType = DataType.Folder;

            //get all MarkDown and HTML content files and create content nodes
            var mdFiles = dataDir.EnumerateFiles(_mdFilter);
            var htmlFiles = dataDir.EnumerateFiles(_htmlFilter);

            foreach (var file in mdFiles.Concat(htmlFiles))
            {
                var name = file.Name.Replace(_htmlExtension, "").Replace(_mdExtension, "");
                var path = string.IsNullOrWhiteSpace(RelativePath) ? "" : RelativePath + Path.DirectorySeparatorChar;
                Children.Add(new ContentNode()
                {
                    Dir = dataDir,
                    Root = root,
                    AbsolutePath = file.FullName,
                    RelativePath = path + name.URLFriendly() + _htmlExtension,
                    Name = name,
                    UrlName = name.URLFriendly() + _htmlExtension,
                    ContentType = file.Name.EndsWith(_htmlExtension) ? DataType.Html : DataType.Markdown
                }
                );
            }

            foreach (var dir in dataDir.GetDirectories())
            {
                Children.Add(new ContentNode(dir, root));
            }

        }

        public string AbsolutePath { get; private set; }
        public string RelativePath { get; private set; }
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

        public string Name { get; private set; }
        public string UrlName { get; private set; }

        public DataType ContentType { get; private set; }

        public List<ContentNode> Children { get; } = new List<ContentNode>();

        private static string GetRelativePath(string filespec, string folder)
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
    }

    public enum DataType
    {
        Markdown,
        Html,
        Folder
    }
}
