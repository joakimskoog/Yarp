using System;

namespace Yarp.Plugins.Reflection
{
    /// <summary>
    /// Contains a method to create instances from given types.
    /// </summary>
    public interface IYarpActivator
    {
        /// <summary>
        /// Creates an instance of the specified type using that type's default constructor.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        T CreateInstance<T>(Type type) where T : class;
    }
}