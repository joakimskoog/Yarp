using System;
using Yarp.Plugins.Api;

namespace Yarp.Plugins
{
    /// <summary>
    /// Class that contains the <seealso cref="IYarpPlugin"/> and its <seealso cref="YarpPluginMetadata"/>.
    /// </summary>
    public class YarpPluginContainer
    {
        public IYarpPlugin Plugin { get; private set; }
        public YarpPluginMetadata Metadata { get; private set; }

        public YarpPluginContainer(IYarpPlugin plugin, YarpPluginMetadata metadata)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (metadata == null) throw new ArgumentNullException(nameof(metadata));
            Plugin = plugin;
            Metadata = metadata;
        }
    }
}