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


    }
}
