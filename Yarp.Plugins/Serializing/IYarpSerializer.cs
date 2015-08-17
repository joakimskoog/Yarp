namespace Yarp.Plugins.Serializing
{
    /// <summary>
    /// Contains logic for serializing and deserializing objects.
    /// </summary>
    public interface IYarpSerializer
    {
        string Serialize<T>(T @object);

        T Deserialize<T>(string data);
    }
}