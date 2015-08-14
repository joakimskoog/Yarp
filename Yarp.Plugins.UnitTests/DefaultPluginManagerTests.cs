using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Yarp.Plugins.Api;

namespace Yarp.Plugins.UnitTests
{
    [TestClass]
    public class DefaultPluginManagerTests
    {
        #region GetAllPlugins

        [TestMethod]
        public void GivenThatNoPluginsExists_WhenGetAllPluginsIsCalledAfterLoading_ThenTheReturnedListIsEmpty()
        {
            var sut = new DefaultPluginManager();
            var pluginLoaderStub = MockRepository.GenerateStub<IPluginLoader>();
            pluginLoaderStub.Stub(x => x.LoadPlugins()).Return(Enumerable.Empty<YarpPluginContainer>());
            sut.PluginLoader = pluginLoaderStub;

            sut.LoadPlugins();
            var plugins = sut.GetAllPlugins();

            Assert.AreEqual(0, plugins.Count());
        }

        [TestMethod]
        public void GivenThatOnePluginExists_WhenGetAllPluginsIsCalledAfterLoading_ThenTheReturnedListContainsOnePlugin()
        {
            var sut = new DefaultPluginManager();
            var pluginLoaderStub = MockRepository.GenerateStub<IPluginLoader>();
            pluginLoaderStub.Stub(x => x.LoadPlugins()).Return(new List<YarpPluginContainer> { new YarpPluginContainer(MockRepository.GenerateStub<IYarpPlugin>(), new YarpPluginMetadata("id", "name", "author", "desc", "version", "categoryKD")) });
            sut.PluginLoader = pluginLoaderStub;

            sut.LoadPlugins();
            var plugins = sut.GetAllPlugins();

            Assert.AreEqual(1, plugins.Count());
        }

        #endregion

        #region GetPluginByName

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenThatNoPluginsExists_WhenGetPluginByNameIsCalled_ThenAnExceptionIsThrown()
        {
            var sut = new DefaultPluginManager();
            var pluginLoaderStub = MockRepository.GenerateStub<IPluginLoader>();
            pluginLoaderStub.Stub(x => x.LoadPlugins()).Return(Enumerable.Empty<YarpPluginContainer>());
            sut.PluginLoader = pluginLoaderStub;

            sut.LoadPlugins();
            var plugin = sut.GetPluginByName("name");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenThatPluginsExistsButNotWithTheGivenName_WhenGetPluginByNameIsCalled_ThenAnExceptionIsThrown()
        {
            var sut = new DefaultPluginManager();
            var pluginLoaderStub = MockRepository.GenerateStub<IPluginLoader>();
            pluginLoaderStub.Stub(x => x.LoadPlugins()).Return(new[] {new YarpPluginContainer(MockRepository.GenerateStub<IYarpPlugin>(), new YarpPluginMetadata("id","testName",
                "author", "description", "version", "category"))});
            sut.PluginLoader = pluginLoaderStub;

            sut.LoadPlugins();
            var plugin = sut.GetPluginByName("name");
        }

        [TestMethod]
        public void GivenThatPluginsExistsWithCorrectName_WhenGetPluginByNameIsCalled_ThenTheExistingPluginIsReturned()
        {
            var sut = new DefaultPluginManager();
            var pluginLoaderStub = MockRepository.GenerateStub<IPluginLoader>();
            YarpPluginContainer existingPlugin = new YarpPluginContainer(MockRepository.GenerateStub<IYarpPlugin>(), new YarpPluginMetadata("id", "testName",
                "author", "description", "version", "category"));
            pluginLoaderStub.Stub(x => x.LoadPlugins()).Return(new[] {existingPlugin});
            sut.PluginLoader = pluginLoaderStub;

            sut.LoadPlugins();
            var plugin = sut.GetPluginByName("testName");

            Assert.AreEqual(existingPlugin.Metadata.Id, plugin.Metadata.Id);
            Assert.AreEqual(existingPlugin.Metadata.Name, plugin.Metadata.Name);
            Assert.AreEqual(existingPlugin.Metadata.Author, plugin.Metadata.Author);
            Assert.AreEqual(existingPlugin.Metadata.Description, plugin.Metadata.Description);
            Assert.AreEqual(existingPlugin.Metadata.Version, plugin.Metadata.Version);
            Assert.AreEqual(existingPlugin.Metadata.Category, plugin.Metadata.Category);
            Assert.AreEqual(existingPlugin.Metadata.Website, plugin.Metadata.Website);
        }

        #endregion
    }
}

