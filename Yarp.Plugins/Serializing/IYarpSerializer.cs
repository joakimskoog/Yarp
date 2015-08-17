namespace Yarp.Plugins.Serializing
{
    public interface IYarpSerializer
    {
        string Serialize<T>(T @object);

        T Deserialize<T>(string data);
    }
}