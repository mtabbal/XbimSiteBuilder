﻿using HeyRed.MarkdownSharp;
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
using Xbim.SiteBuilder.Structure;

namespace Xbim.SiteBuilder
{
    class Program
    {
       
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
            var contentRoot = new DirectoryNode(dataDir, dataDir, null);
            contentRoot.Render(rootDir);
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
