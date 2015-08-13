using System.Collections.Generic;

namespace Yarp.Plugins
{
    /// <summary>
    /// Responsible for loading plugins.
    /// </summary>
    public interface IPluginLoader
    {
        /// <summary>
        /// Loads all the available plugins along with their metadata.
        /// </summary>
        /// <returns>The available plugins along with their metadata.</returns>
        IEnumerable<YarpPluginContainer> LoadPlugins();
    }
}