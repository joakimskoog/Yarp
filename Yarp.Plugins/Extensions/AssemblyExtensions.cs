using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yarp.Plugins.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetExportedTypesAssignableFrom<T>(this Assembly assembly)
        {
            if (assembly == null) return Enumerable.Empty<Type>();

            return assembly.GetExportedTypes().Where(type => typeof (T).IsAssignableFrom(type));
        }
    }
}