using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Yarp.IO;
using Yarp.Plugins.Api;
using Yarp.Plugins.Reflection;
using Yarp.Plugins.Serializing;

namespace Yarp.Plugins.UnitTests
{
    [TestClass]
    public class ReflectionBasedPluginLoaderTests
    {
        [TestMethod]
        public void GivenThatPluginsFolderDoesNotExist_WhenLoadPluginsIsCalled_ThenTheReturnedListIsEmpty()
        {
            var settings = new ReflectionBasedPluginLoaderSettings("dummyPath", "*YarpPlugin.dll", ".YarpMetadata");
            var directoryStub = MockRepository.GenerateStub<DirectoryBase>();
            directoryStub.Stub(x => x.Exists(settings.PluginsPath)).Return(false);
            var sut = CreateSut(settings, directory: directoryStub);

            var loadedPlugins = sut.LoadPlugins().ToList();

            Assert.AreEqual(0, loadedPlugins.Count);
        }

        [TestMethod]
        public void GivenThatPluginsFolderExistsButContainsNoFolders_WhenLoadPluginsIsCalled_ThenTheReturnedListIsEmpty()
        {
            var settings = new ReflectionBasedPluginLoaderSettings("dummyPath", "*YarpPlugin.dll", ".YarpMetadata");
            var directoryStub = MockRepository.GenerateStub<DirectoryBase>();
            directoryStub.Stub(x => x.Exists(settings.PluginsPath)).Return(true);
            directoryStub.Stub(x => x.EnumerateDirectories(settings.PluginsPath)).Return(Enumerable.Empty<string>());
            var sut = CreateSut(settings, directory: directoryStub);

            var loadedPlugins = sut.LoadPlugins().ToList();

            Assert.AreEqual(0, loadedPlugins.Count);
        }

        //Finds one folder but it contains no dll file matching search pattern and no metadata file matching search pattern: return empty

        [TestMethod]
        public void GivenThatPluginsFolderExistsAndItContainsOneSubFolderButNoFiles_WhenLoadPluginsIsCalled_ThenTheReturnedListIsEmpty()
        {
            const string folderContainingNoPlugins = "folderContainingNoPlugins";

            var settings = new ReflectionBasedPluginLoaderSettings("dummyPath", "dummyPluginPattern", "dummyMetadaPattern");
            var directoryStub = MockRepository.GenerateStub<DirectoryBase>();
            directoryStub.Stub(x => x.Exists(settings.PluginsPath)).Return(true);
            directoryStub.Stub(x => x.EnumerateDirectories(settings.PluginsPath)).Return(new[] { folderContainingNoPlugins });
            directoryStub.Stub(x => x.EnumerateFiles(folderContainingNoPlugins, settings.PluginFileSearchPattern, SearchOption.TopDirectoryOnly)).Return(Enumerable.Empty<string>());
            directoryStub.Stub(x => x.EnumerateFiles(folderContainingNoPlugins, settings.PluginMetadataSearchPattern,
                        SearchOption.TopDirectoryOnly)).Return(Enumerable.Empty<string>());
            var sut = CreateSut(settings, directory: directoryStub);

            var loadedPlugins = sut.LoadPlugins().ToList();

            Assert.AreEqual(0, loadedPlugins.Count);
        }

        [TestMethod]
        public void GivenThatPluginFileExistsButFileContainsNoPlugin_WhenLoadPluginsIsCalled_ThenTheReturnedListIsEmpty()
        {
            const string folderContainingPlugins = "folderContainingPlugins";
            const string pluginFile = "pluginFile";
            const string metadataFile = "metadataFile";

            var settings = new ReflectionBasedPluginLoaderSettings("dummyPath", "dummyPluginpattern", "dummyMetadataPattern");
            var directoryStub = MockRepository.GenerateStub<DirectoryBase>();
            directoryStub.Stub(x => x.Exists(settings.PluginsPath)).Return(true);
            directoryStub.Stub(x => x.EnumerateDirectories(settings.PluginsPath))
                .Return(new[] { folderContainingPlugins });
            directoryStub.Stub(
                x =>
                    x.EnumerateFiles(folderContainingPlugins, settings.PluginFileSearchPattern,
                        SearchOption.TopDirectoryOnly))
                .Return(new[] {pluginFile});
            directoryStub.Stub(
                x =>
                    x.EnumerateFiles(folderContainingPlugins, settings.PluginMetadataSearchPattern,
                        SearchOption.TopDirectoryOnly))
                .Return(new[] {metadataFile});

            var assemblyLoaderStub = MockRepository.GenerateStub<IAssemblyLoader>();
            assemblyLoaderStub.Stub(x => x.LoadFile(pluginFile)).Return(null);
            var activatorStub = MockRepository.GenerateStub<IYarpActivator>();
            activatorStub.Stub(x => x.CreateInstance<IYarpPlugin>(null)).IgnoreArguments().Return(null);

            var sut = CreateSut(settings, activator: activatorStub, loader: assemblyLoaderStub, directory: directoryStub);

            var loadedPlugins = sut.LoadPlugins().ToList();

            Assert.AreEqual(0, loadedPlugins.Count);

        }

        [TestMethod]
        public void GivenThatFoldersAreCorrectAndPluginAndMetadataExists_WhenLoadPluginsIsCalled_TheReturnedListContainsOnePluginContainer()
        {
            const string folderContainingPlugins = "folderContainingPlugins";
            const string pluginFile = "pluginFile";
            const string metadataFile = "metadataFile";

            var settings = new ReflectionBasedPluginLoaderSettings("dummyPath", "dummyPluginpattern", "dummyMetadataPattern");
            var directoryStub = MockRepository.GenerateStub<DirectoryBase>();
            directoryStub.Stub(x => x.Exists(settings.PluginsPath)).Return(true);
            directoryStub.Stub(x => x.EnumerateDirectories(settings.PluginsPath))
                .Return(new[] { folderContainingPlugins });
            directoryStub.Stub(
                x =>
                    x.EnumerateFiles(folderContainingPlugins, settings.PluginFileSearchPattern,
                        SearchOption.TopDirectoryOnly))
                .Return(new[] { pluginFile });
            directoryStub.Stub(
                x =>
                    x.EnumerateFiles(folderContainingPlugins, settings.PluginMetadataSearchPattern,
                        SearchOption.TopDirectoryOnly))
                .Return(new[] { metadataFile });

            var plugin = new DummyPlugin();
            var metadata = new YarpPluginMetadata("id", "name", "author", "description", "version", "category",
                "website");

            var assemblyLoaderStub = MockRepository.GenerateStub<IAssemblyLoader>();
            assemblyLoaderStub.Stub(x => x.LoadFile(pluginFile)).Return(null);
            var activatorStub = MockRepository.GenerateStub<IYarpActivator>();
            activatorStub.Stub(x => x.CreateInstance<IYarpPlugin>(null)).IgnoreArguments().Return(plugin);
            var serializerStub = MockRepository.GenerateStub<IYarpSerializer>();
            serializerStub.Stub(x => x.Deserialize<YarpPluginMetadata>("")).IgnoreArguments().Return(metadata);
            var fileStub = MockRepository.GenerateStub<FileBase>();
            fileStub.Stub(x => x.ReadAllText("")).IgnoreArguments().Return("someFileData");

            var sut = CreateSut(settings, activator: activatorStub, loader: assemblyLoaderStub, directory: directoryStub, file:fileStub,
                serializer:serializerStub);

            var loadedPlugins = sut.LoadPlugins().ToList();
            var loadedMetadata = loadedPlugins[0].Metadata;
            var loadedPlugin = loadedPlugins[0].Plugin;

            loadedPlugin.Activate();
            loadedPlugin.Deactivate();

            Assert.AreEqual(1, loadedPlugins.Count);
            Assert.IsTrue(ArePluginMetadatasEqual(metadata, loadedMetadata));
            Assert.IsTrue(plugin.HasBeenActivated);
            Assert.IsTrue(plugin.HasBeenDeactivated);
        }

        private bool ArePluginMetadatasEqual(YarpPluginMetadata first, YarpPluginMetadata second)
        {
            return first.Id.Equals(second.Id) &&
                first.Name.Equals(second.Name) &&
                first.Author.Equals(second.Author) &&
                first.Description.Equals(second.Description) &&
                first.Version.Equals(second.Version) &&
                first.Category.Equals(second.Category) &&
                first.Website.Equals(second.Website);
        }

        private ReflectionBasedPluginLoader CreateSut(ReflectionBasedPluginLoaderSettings settings, IYarpSerializer serializer = null, IYarpActivator activator = null,
            IAssemblyLoader loader = null, DirectoryBase directory = null, FileBase file = null)
        {
            var yarpSerializer = serializer ?? MockRepository.GenerateStub<IYarpSerializer>();
            var yarpActivator = activator ?? MockRepository.GenerateStub<IYarpActivator>();
            var assemblyLoader = loader ?? MockRepository.GenerateStub<IAssemblyLoader>();
            var directoryBase = directory ?? MockRepository.GenerateStub<DirectoryBase>();
            var fileBase = file ?? MockRepository.GenerateStub<FileBase>();

            return new ReflectionBasedPluginLoader(settings, yarpSerializer, yarpActivator, assemblyLoader, directoryBase, fileBase);
        }

        public class DummyPlugin : IYarpPlugin
        {
            public bool HasBeenActivated { get; private set; }
            public bool HasBeenDeactivated { get; private set; }

            public void Activate()
            {
                HasBeenActivated = true;
            }

            public void Deactivate()
            {
                HasBeenDeactivated = true;
            }

            public IEnumerable<PasswordRetrievalResult> RetrievePasswords()
            {
                return new[] {new PasswordRetrievalResult("username", "password", "source")};
            }
        }
    }
}
