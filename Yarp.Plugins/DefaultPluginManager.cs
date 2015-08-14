using System;
using System.Collections.Generic;
using System.Linq;

namespace Yarp.Plugins
{
    public class DefaultPluginManager : IPluginManager
    {
        private IList<YarpPluginContainer> _plugins;

        private IPluginLoader _pluginLoader;
        public IPluginLoader PluginLoader
        {
            get { return _pluginLoader ?? (_pluginLoader = new ReflectionBasedPluginLoader()); }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _pluginLoader = value;
            }
        }

        public DefaultPluginManager()
        {
            _plugins = new List<YarpPluginContainer>();
        }

        public IEnumerable<YarpPluginContainer> GetAllPlugins()
        {
            return _plugins;
        }

        public IEnumerable<YarpPluginContainer> GetAllPluginsByCategory(string category)
        {
            return _plugins.Where(p => string.Equals(p.Metadata.Category, category, StringComparison.InvariantCultureIgnoreCase));
        }

        public YarpPluginContainer GetPluginByName(string name)
        {
            var plugin = _plugins.FirstOrDefault(p => string.Equals(p.Metadata.Name, name, StringComparison.InvariantCultureIgnoreCase));

            if (plugin == null)
            {
                throw new Exception(string.Format("No plugin with the given name '{0}' exists.", name));
            }

            return plugin;
        }

        public YarpPluginContainer GetPluginById(string id)
        {
            var plugin = _plugins.FirstOrDefault(p => string.Equals(p.Metadata.Id, id, StringComparison.InvariantCultureIgnoreCase));

            if (plugin == null)
            {
                throw new Exception(string.Format("No plugin with the given id '{0}' exists.", id));
            }

            return plugin;
        }

        public void LoadPlugins()
        {
            DeactivePlugins(_plugins);
            _plugins = PluginLoader.LoadPlugins().ToList();
            ActivatePlugins(_plugins);
        }

        private void ActivatePlugins(IEnumerable<YarpPluginContainer> plugins)
        {
            foreach (var plugin in plugins)
            {
                plugin.Plugin.Activate();
            }
        }

        private void DeactivePlugins(IEnumerable<YarpPluginContainer> plugins)
        {
            foreach (var plugin in plugins)
            {
                plugin.Plugin.Deactivate();
            }
        }
    }
}