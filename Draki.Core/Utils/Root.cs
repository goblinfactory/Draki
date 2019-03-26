using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Draki
{
    public class Solution
    {
        private static string _root = null;
        private const string DEFAULT_ROOT_MARKER = ".gitignore";
        private readonly string _rootMarkerFile;

        /// <summary>
        /// returns the solution root based on the filepath and a rootMarkerFile.
        /// </summary>
        /// <param name="rootMarkerFile">A file you know is in the root of the solution, e.g. gitignore.</param>
        public Solution(string rootMarkerFile = DEFAULT_ROOT_MARKER)
        {
            _rootMarkerFile = rootMarkerFile;
        }
        private static string FilePath([CallerFilePath] string path = null) => path;

        public string Root
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_root)) return _root;
                var dir = FilePath();
                bool isroot = false;
                while(!isroot)
                {
                    var parent = new DirectoryInfo(dir).Parent;
                    var rootfile = Path.Combine(parent.FullName, _rootMarkerFile);
                    if(File.Exists(rootfile))
                    {
                        _root = parent.FullName;
                        return _root;
                    }
                    dir = parent.FullName;
                }
                return dir;
            }
        }
    }
}
