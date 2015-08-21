using System.Collections.Generic;

namespace Yarp.Plugins
{
    /// <summary>
    /// Manages all the plugins.
    /// </summary>
    public interface IPluginManager
    {
        /// <summary>
        /// The loader that is used to load plugins. 
        /// </summary>
        IPluginLoader PluginLoader { get; set; }

        /// <summary>
        /// Retrieves all available plugins along with their metadata.
        /// </summary>
        /// <returns></returns>
        IEnumerable<YarpPluginContainer> GetAllPlugins();

        /// <summary>
        /// Retrieves all the plugins along with their metadata with the given category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        IEnumerable<YarpPluginContainer> GetAllPluginsByCategory(string category);

        /// <summary>
        /// Retrieves the plugin along with its metadata with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        YarpPluginContainer GetPluginByName(string name);

        /// <summary>
        /// Retrieves the plugin along with its metadata with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        YarpPluginContainer GetPluginById(string id);

        /// <summary>
        /// Loads the available plugins.
        /// </summary>
        void LoadPlugins();
    }
}