using System;
using System.Collections.Generic;
using System.Linq;

namespace Yarp.Plugins
{
    public class ReflectionBasedPluginLoader : IPluginLoader
    {
        private readonly ReflectionBasedPluginLoaderSettings _settings;

        public ReflectionBasedPluginLoader(ReflectionBasedPluginLoaderSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            _settings = settings;
        }

        public IEnumerable<YarpPluginContainer> LoadPlugins()
        {
            return Enumerable.Empty<YarpPluginContainer>();
        }
    }
}