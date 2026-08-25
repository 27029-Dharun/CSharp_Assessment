using System.Text.Json;
using System.Text.Json.Serialization;
using ToDoListApplication.Models;

namespace ToDoListApplication.Repository
{
    internal class TaskRepository
    {
        private readonly string _userFilePath;
        private readonly List<ToDoTask> _tasks;
        public TaskRepository(string path)
        {
            _userFilePath = path;
            _tasks = LoadAll(_userFilePath);
        }
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() },
        };

        /// <summary>
        /// Writes all the task to the file
        /// </summary>
        /// <param name="filePath">The path of the file where the task are stored</param>
        /// <param name="list">List of the tasks that are to be added</param>
        public static void WriteAll(string filePath, List<ToDoTask> list)
        {
            string json = JsonSerializer.Serialize(list, _options);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Loads all the content and loads into the file
        /// </summary>
        /// <param name="filePath">Path of the file from which the contents are loaded </param>
        /// <returns>A list of tasks that are stored in the file</returns>
        public static List<ToDoTask> LoadAll(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, string.Empty);
                return new List<ToDoTask>();
            }

            string text = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(text))
            {
                return new List<ToDoTask>();
            }
            List<ToDoTask>? tasks = JsonSerializer.Deserialize<List<ToDoTask>>(text, _options);
            if (tasks is null)
            {
                return new List<ToDoTask>();
            }

            return tasks;
        }

        public void Add(ToDoTask task)
        {
            _tasks.Add(task);
            WriteAll(_userFilePath, _tasks);
        }

        internal List<string> GetAllTaskTitle(Guid userId)
        {
            return _tasks.Select(task => task.Title).ToList();
        }

        internal ToDoTask? GetById(Guid id)
        {
            return _tasks.FirstOrDefault(x => x.Id == id);
        }

        internal List<ToDoTask> GetByUserId(Guid userId)
        {
            return _tasks.Where(x => x.UserId == userId).ToList();
        }

        internal void Update(ToDoTask task)
        {
            ToDoTask? existingTask = this.GetById(task.Id);
            if (existingTask is null)
            {
                return;
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Date = task.Date;

        }

        internal void Delete(Guid id)
        {
            ToDoTask? task = this.GetById(id);
            if (task is null)
            {
                return;
            }

            _tasks.Remove(task);
        }
    }
}
