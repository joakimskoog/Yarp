using System.Reflection;

namespace Yarp.Plugins.Reflection
{
    public class AssemblyWrapper : IAssemblyLoader
    {
        public Assembly LoadFile(string path)
        {
            return Assembly.LoadFile(path);
        }
    }
}