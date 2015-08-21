using System;

namespace Yarp.Plugins
{
    public class ReflectionBasedPluginLoaderSettings
    {
        /// <summary>
        /// The path to the folder where the plugins reside
        /// </summary>
        public string PluginsPath { get; private set; }

        /// <summary>
        /// The search pattern that will be used to find plugins
        /// </summary>
        public string PluginFileSearchPattern { get; private set; }

        /// <summary>
        /// The search pattern that will beu sed to find metadata for plugins
        /// </summary>
        public string PluginMetadataSearchPattern { get; private set; }

        /// <summary>
        /// Constructs a new <see cref="ReflectionBasedPluginLoaderSettings"/> which can be used in the <seealso cref="ReflectionBasedPluginLoader"/>
        /// </summary>
        /// <param name="pluginsPath">The path to the folder where the plugins reside</param>
        /// <param name="pluginFileSearchPattern">The search pattern that will be used to find plugins</param>
        /// <param name="pluginMetadataSearchPattern">The search pattern that will beu sed to find metadata for plugins</param>
        public ReflectionBasedPluginLoaderSettings(string pluginsPath, string pluginFileSearchPattern, string pluginMetadataSearchPattern)
        {
            if (string.IsNullOrEmpty(pluginsPath)) throw new ArgumentException("PluginsPath can't be empty", nameof(pluginsPath));
            if (string.IsNullOrEmpty(pluginFileSearchPattern)) throw new ArgumentException("PluginFileSearchPattern can't be empty", nameof(pluginFileSearchPattern));
            if (string.IsNullOrEmpty(pluginMetadataSearchPattern)) throw new ArgumentException("PluginMetadataSearchPattern can't be empty", nameof(pluginMetadataSearchPattern));
            PluginsPath = pluginsPath;
            PluginFileSearchPattern = pluginFileSearchPattern;
            PluginMetadataSearchPattern = pluginMetadataSearchPattern;
        }
    }
}
