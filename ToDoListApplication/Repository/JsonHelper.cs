using System.Text.Json;

namespace ToDoListApplication.Repository
{
    public static class JsonHelper
    {
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        public static List<T> ReadAll<T>(string path)
        {
            if (!File.Exists(path))
            {
                // create an empty file
                File.WriteAllText(path, string.Empty);
            }

            string json = File.ReadAllText(path);
            if (string.IsNullOrEmpty(json))
            {
                return new List<T>();
            }

            List<T> list = JsonSerializer.Deserialize<List<T>>(json, options) ?? new List<T>();
            return list;
        }

        public static void WriteAll<T>(string path, List<T> list)
        {
            string json = JsonSerializer.Serialize(list, options);
            File.WriteAllText(path, json);
        }
    }
}
