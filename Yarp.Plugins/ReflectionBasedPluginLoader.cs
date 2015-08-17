using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Yarp.IO;
using Yarp.Plugins.Api;
using Yarp.Plugins.Extensions;
using Yarp.Plugins.Reflection;
using Yarp.Plugins.Serializing;

namespace Yarp.Plugins
{
    public class ReflectionBasedPluginLoader : IPluginLoader
    {
        private static readonly object _mutex = new object();
        private static ReflectionBasedPluginLoader _default;
        public static ReflectionBasedPluginLoader Default
        {
            get
            {
                lock (_mutex)
                {
                    return _default ??
                           (_default = new ReflectionBasedPluginLoader(new ReflectionBasedPluginLoaderSettings(
                               "plugins", "*YarpPlugin.dll", "*.YarpMetadata"), new JsonSerializer(),
                               new ActivatorWrapper(), new AssemblyWrapper(),
                               new DirectoryWrapper(), new FileWrapper()));
                }
            }
        }

        private readonly ReflectionBasedPluginLoaderSettings _settings;
        private readonly IYarpSerializer _serializer;
        private readonly IYarpActivator _activator;
        private readonly IAssemblyLoader _assemblyLoader;
        private readonly DirectoryBase _directory;
        private readonly FileBase _file;

        public ReflectionBasedPluginLoader(ReflectionBasedPluginLoaderSettings settings, IYarpSerializer serializer, IYarpActivator activator,
            IAssemblyLoader assemblyLoader, DirectoryBase directory, FileBase file)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            if (serializer == null) throw new ArgumentNullException("serializer");
            if (activator == null) throw new ArgumentNullException("activator");
            if (assemblyLoader == null) throw new ArgumentNullException("assemblyLoader");
            if (directory == null) throw new ArgumentNullException("directory");
            if (file == null) throw new ArgumentNullException("file");
            _settings = settings;
            _serializer = serializer;
            _activator = activator;
            _assemblyLoader = assemblyLoader;
            _directory = directory;
            _file = file;
        }

        public IEnumerable<YarpPluginContainer> LoadPlugins()
        {
            if (!_directory.Exists(_settings.PluginsPath)) yield break;

            foreach (var folder in _directory.EnumerateDirectories(_settings.PluginsPath))
            {
                var possiblePluginFiles = _directory.EnumerateFiles(folder, _settings.PluginFileSearchPattern, SearchOption.TopDirectoryOnly).ToList();
                var possibleMetadataFiles = _directory.EnumerateFiles(folder, _settings.PluginMetadataSearchPattern, SearchOption.TopDirectoryOnly).ToList();

                if (possiblePluginFiles.Count == 1 && possibleMetadataFiles.Count == 1)
                {
                    var plugin = GetPlugin(possiblePluginFiles[0]);
                    var metadata = GetMetadata(possibleMetadataFiles[0]);

                    if (plugin != null && metadata != null)
                    {
                        yield return new YarpPluginContainer(plugin, metadata);
                    }
                }
            }
        }

        private IYarpPlugin GetPlugin(string filePath)
        {
            var fullFilePath = Path.GetFullPath(filePath);
            var pluginAssembly = _assemblyLoader.LoadFile(fullFilePath);
            var type = pluginAssembly.GetExportedTypesAssignableFrom<IYarpPlugin>().FirstOrDefault();

            return _activator.CreateInstance<IYarpPlugin>(type);
        }

        private YarpPluginMetadata GetMetadata(string filePath)
        {
            var fullFilePath = Path.GetFullPath(filePath);
            var fileContents = _file.ReadAllText(fullFilePath, Encoding.UTF8);

            return _serializer.Deserialize<YarpPluginMetadata>(fileContents);
        }
    }
}