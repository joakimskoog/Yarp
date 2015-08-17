using System.Reflection;

namespace Yarp.Plugins.Reflection
{
    public interface IAssemblyLoader
    {
        Assembly LoadFile(string path);
    }
}