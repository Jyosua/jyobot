using System.IO;
using Newtonsoft.Json;

namespace Jyobot.Workers
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string filename)
        {
            using var reader = new StreamReader(filename);
            using var jsonReader = new JsonTextReader(reader);

            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<T>(jsonReader);
        }

        public static void Serialize<T>(T instance, string filename)
        {
            using var writer = new StreamWriter(filename);

            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(writer, instance);
        }
    }
}