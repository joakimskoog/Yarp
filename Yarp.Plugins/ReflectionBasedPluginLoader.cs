using System.Collections.Generic;
using System.Linq;

namespace Yarp.Plugins
{
    public class ReflectionBasedPluginLoader : IPluginLoader
    {
        public IEnumerable<YarpPluginContainer> LoadPlugins()
        {
            return Enumerable.Empty<YarpPluginContainer>();
        }
    }
}