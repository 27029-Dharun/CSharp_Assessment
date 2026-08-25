using ToDoListApplication.Models;

namespace ToDoListApplication.Repository
{
    internal class TaskRepository
    {
        private readonly string _userFilePath;
        private readonly List<Task> _tasks;
        public TaskRepository(string path)
        {
            _userFilePath = path;
            _tasks = JsonHelper.ReadAll<Task>(_userFilePath);
        }

        public void Add(Task user)
        {
            _tasks.Add(user);
            JsonHelper.WriteAll(_userFilePath, _tasks);
        }

        public Task? GetByTaskName(string userName)
        {
            return _tasks.FirstOrDefault(x => x.TaskName == userName);
        }

        internal List<string> GetByTaskName()
        {
            return _tasks.Select(x => x.TaskName).ToList();
        }
    }
}

