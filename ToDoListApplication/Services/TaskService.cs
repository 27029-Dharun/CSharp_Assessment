using ToDoListApplication.Models;
using ToDoListApplication.Models.Enums;
using ToDoListApplication.Repository;
using ToDoListApplication.Validators;

namespace ToDoListApplication.Services
{
    internal class TaskService
    {
        private readonly TaskRepository _taskRepository;

        /// <summary>
        /// Service that handles business logic for the tasks
        /// </summary>
        /// <param name="repository"></param>
        public TaskService(TaskRepository repository)
        {
            _taskRepository = repository;
        }

        /// <summary>
        /// Add recurrence task
        /// </summary>
        /// <param name="userId">Unique user id</param>
        /// <param name="title">Title of the task</param>
        /// <param name="description">Description of the task</param>
        /// <param name="targetDate">Target date</param>
        /// <param name="recurrence">Recurrence period</param>
        /// <returns></returns>
        public bool AddRecurrenceTask(Guid userId, string title, string description, DateTime targetDate, Recurrence recurrence)
        {
            List<string> titleList = this._taskRepository.GetAllTaskTitle(userId);
            if (!Validator.IsUniqueField(title, titleList))
            {
                return false;
            }

            if (DateTime.Today > targetDate)
            {
                return false;
            }

            DateTime current = DateTime.Today;
            while (true)
            {
                DateTime taskDate = current;
                this.AddTask(userId, title, description, taskDate);

                switch (recurrence)
                {
                    case Recurrence.Daily:
                        taskDate = current.AddDays(1);
                        break;

                    case Recurrence.Weekly:
                        taskDate = current.AddDays(7);
                        break;

                    case Recurrence.Monthly:
                        taskDate = current.AddMonths(1);
                        break;

                    case Recurrence.Yearly:
                        taskDate = current.AddYears(1);
                        break;
                }

                if (taskDate > targetDate)
                {
                    break;
                }

                current = taskDate;
            }

            return true;
        }

        /// <summary>
        /// Lists all the tasks that are created
        /// </summary>
        /// <param name="userId">Unique identifier of user</param>
        /// <returns>A list of task that is belonging to the user</returns>
        public List<ToDoTask> ViewUserTask(Guid userId)
        {
            return this._taskRepository.GetByUserId(userId);
        }

        internal void DeleteTask(Guid id)
        {
            this._taskRepository.Delete(id);
        }

        internal void GetDashboard()
        {
            throw new NotImplementedException();
        }

        internal bool GetTaskExist(Guid userId)
        {
            return this._taskRepository.HasAny(userId);
        }

        internal bool UpdateTask(ToDoTask task)
        {
            this._taskRepository.Update(task);
            return true;
        }

        private void AddTask(Guid userId, string title, string description, DateTime taskDate)
        {
            ToDoTask task = new ToDoTask(Guid.NewGuid(), userId, title, description, taskDate);

            this._taskRepository.Add(task);
        }
    }
}
