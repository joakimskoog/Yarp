using System;
using System.Collections.Generic;
using System.IO;

namespace Yarp.IO
{
    public abstract class DirectoryBase
    {
        public abstract bool Exists(string path);

        public abstract IEnumerable<String> EnumerateDirectories(String path);
        public abstract IEnumerable<String> EnumerateDirectories(String path, String searchPattern);
        public abstract IEnumerable<String> EnumerateDirectories(String path, String searchPattern, SearchOption searchOption);

        public abstract IEnumerable<string> EnumerateFiles(string path);
        public abstract IEnumerable<string> EnumerateFiles(string path, string searchPattern);
        public abstract IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption);
    }
}