using System;

namespace Yarp.Plugins.Reflection
{
    public class ActivatorWrapper : IYarpActivator
    {
        public T CreateInstance<T>(Type type) where T : class
        {
            if (type == null) return null;
            return (T)Activator.CreateInstance(type);
        }
    }
}