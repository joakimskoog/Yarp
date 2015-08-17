using Newtonsoft.Json;

namespace Yarp.Plugins.Serializing
{
    public class JsonSerializer : IYarpSerializer
    {
        public string Serialize<T>(T @object)
        {
            return JsonConvert.SerializeObject(@object, Formatting.Indented);
        }

        public T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}