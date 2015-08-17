using System;
using System.Collections.Generic;
using System.IO;

namespace Yarp.IO
{
    /// <summary>
    /// Abstraction of IO that involves directories.
    /// </summary>
    public abstract class DirectoryBase
    {
        /// <summary>
        /// Determines whether the given path refers to an existing directory on disk.
        /// </summary>
        /// <param name="path">The path to test</param>
        /// <returns>True if path refers to an existing directory; false if the directory does not exist</returns>
        public abstract bool Exists(string path);

        /// <summary>
        /// Returns an enumerable collection of directory names in a specified path.
        /// </summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by path.</returns>
        public abstract IEnumerable<String> EnumerateDirectories(String path);

        /// <summary>
        /// Returns an enumerable collection of directory names that match a search pattern in a specified path.
        /// </summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.></param>
        /// <param name="searchPattern">The search string to match against the names of directories in path.</param>
        /// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by path and that match the specified search pattern.</returns>
        public abstract IEnumerable<String> EnumerateDirectories(String path, String searchPattern);

        /// <summary>
        /// Returns an enumerable collection of directory names that match a search pattern in a specified path, and optionally searches subdirectories.
        /// </summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of directories in path.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.</param>
        /// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by path and that match the specified search pattern and option.</returns>
        public abstract IEnumerable<String> EnumerateDirectories(String path, String searchPattern, SearchOption searchOption);

        /// <summary>
        /// Returns an enumerable collection of file names in a specified path.
        /// </summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by path.</returns>
        public abstract IEnumerable<string> EnumerateFiles(string path);

        /// <summary>
        /// Returns an enumerable collection of file names that match a search pattern in a specified path.
        /// </summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in path.</param>
        /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by path and that match the specified search pattern.</returns>
        public abstract IEnumerable<string> EnumerateFiles(string path, string searchPattern);

        /// <summary>
        /// Returns an enumerable collection of file names that match a search pattern in a specified path, and optionally searches subdirectories.
        /// </summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in path.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.</param>
        /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by path and that match the specified search pattern and option.</returns>
        public abstract IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption);
    }
}