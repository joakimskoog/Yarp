using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yarp.Plugins.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Retrieves all exported types that are assignable to T.
        /// </summary>
        /// <typeparam name="T">The type to check if the exported types are assignable to.</typeparam>
        /// <param name="assembly">The assembly whose exported types should be checked</param>
        /// <returns>An enumerable collection that contains all exported types that are assignable to T</returns>
        public static IEnumerable<Type> GetExportedTypesAssignableFrom<T>(this Assembly assembly)
        {
            if (assembly == null) return Enumerable.Empty<Type>();

            return assembly.GetExportedTypes().Where(type => typeof (T).IsAssignableFrom(type));
        }
    }
}