using System.Reflection;

namespace Yarp.Plugins.Reflection
{
    /// <summary>
    /// Contains logic for loading assemblies from files.
    /// </summary>
    public interface IAssemblyLoader
    {
        Assembly LoadFile(string path);
    }
}