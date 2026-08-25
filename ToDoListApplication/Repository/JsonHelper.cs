using System.Text.Json;

namespace ToDoListApplication.Repository
{
    /// <summary>
    /// Contains the helper for the json file handling
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Options to write and read the file
        /// </summary>
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        /// <summary>
        /// Reads the content in the file and write it.
        /// </summary>
        /// <typeparam name="T">Type variable </typeparam>
        /// <param name="path">Path where th file is present</param>
        /// <returns>A list of elements in the file</returns>
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

        /// <summary>
        /// Writes all the content in the file
        /// </summary>
        /// <typeparam name="T">Type variable </typeparam>
        /// <param name="path">Path where th file is present</param>
        /// <param name="list">A list of object that is to be written</param>
        public static void WriteAll<T>(string path, List<T> list)
        {
            string json = JsonSerializer.Serialize(list, options);
            File.WriteAllText(path, json);
        }
    }
}
