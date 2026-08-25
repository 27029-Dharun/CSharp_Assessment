using ToDoListApplication.Models;
using ToDoListApplication.Models.Enums;
using ToDoListApplication.Services;
using ToDoListApplication.View;

namespace ToDoListApplication.Controllers
{
    /// <summary>
    /// controller
    /// </summary>
    internal class TaskController
    {
        private readonly TaskService _taskService;
        private readonly ConsoleView _view;

        /// <summary>
        /// Initialize the object
        /// </summary>
        /// <param name="service"></param>
        /// <param name="view"></param>
        public TaskController(TaskService service, ConsoleView view)
        {
            this._view = view;
            this._taskService = service;
        }

        /// <summary>
        /// Dashboard
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        public void Dashboard(Guid userId, string userName)
        {
            while (true)
            {
                try
                {
                    this.DisplayTask(userId, userName);

                    DashboardOption option = (DashboardOption)this._view.GetOption("1. Add new task\n" +
                        "2. View all task\n" +
                        "3. Remove existing task\n" +
                        "4. Edit existing task\n" +
                        "5. View calendar\n" +
                        "6. LogOut\n");

                    switch (option)
                    {
                        case DashboardOption.Add:
                            this.HandleAddTask(userId);
                            break;

                        case DashboardOption.View:
                            this.HandleView(userId);
                            break;

                        case DashboardOption.Remove:
                            this.HandleRemoveTask(userId);
                            break;

                        case DashboardOption.Update:
                            this.HandleUpdateTask(userId);
                            break;

                        case DashboardOption.Calendar:
                            this.HandleViewCalendar(userId);
                            break;

                        case DashboardOption.LogOut:
                            return;

                        default:
                            this._view.PrintInfo("Enter an valid option");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    this._view.PrintInfo(ex.Message);
                }

                this._view.PauseAndContinue();
            }
        }

        private void HandleAddTask(Guid userId)
        {
            string title = this._view.GetTitle("Enter the title of the task: ");
            string description = this._view.GetDescription("Enter the description: ");
            DateTime date = this._view.GetTargetDate("Enter the target date in format (dd/MM/yyyy) till which we want to create the task: ");
            Recurrence recurrence = this._view.GetRecurrence("Enter the recurrence period: ");

            if (this._taskService.AddRecurrenceTask(userId, title, description, date, recurrence))
            {
                this._view.PrintInfo("Task created successfully");
                return;
            }

            this._view.PrintInfo("Task should have a unique title");
        }

        private void HandleView(Guid userId)
        {
            List<ToDoTask> tasks = this._taskService.GetCalendar(userId);
            if (tasks.Any())
            {

                this._view.PrintTaskTable(tasks);
                return;
            }

            this._view.PrintInfo("No transaction available");
        }

        private void HandleUpdateTask(Guid userId)
        {

            List<ToDoTask> tasks = this._taskService.GetCalendar(userId);
            if (!tasks.Any())
            {
                this._view.PrintInfo("No transaction available");
                return;
            }

            this._view.PrintTaskTable(tasks);
            this._view.PrintInfo("Can edit only the tasks field title, description and date. Can't edit the recurrence type");
            int index = this._view.GetIndex("Enter the index to edit: ");
            if (index >= 0 && index < tasks.Count)
            {
                ToDoTask task = tasks[index];

                int option = this._view.GetOption("1. Title\n2. Description\n3. Date\nEnter the field to edit: ");

                switch (option)
                {
                    case 1:
                        task.Title = this._view.GetTitle("Enter the title: ");
                        break;

                    case 2:
                        task.Description = this._view.GetDescription("Enter the description: ");
                        break;

                    case 3:
                        task.Date = this._view.GetTargetDate("Enter the date: ");
                        break;

                    default:
                        this._view.PrintInfo("You selected an invalid option please try again");
                        break;
                }

                if (this._taskService.UpdateTask(task))
                {
                    this._view.PrintInfo("Updated successfully");
                    return;
                }
            }

            this._view.PrintInfo("Failed to edit");
            return;
        }

        private void HandleRemoveTask(Guid userId)
        {
            List<ToDoTask> tasks = this._taskService.GetCalendar(userId);

            if (!tasks.Any())
            {
                this._view.PrintInfo("No transaction available");
                return;
            }

            this._view.PrintTaskTable(tasks);

            int index = this._view.GetIndex("Enter the index to edit: ");
            if (index >= 0 && index < tasks.Count)
            {
                Guid id = tasks[index].Id;
                this._taskService.DeleteTask(id);
                this._view.PrintInfo("Deleted successfully");
                return;
            }

            this._view.PrintInfo("Failed to delete");
            return;
        }

        private void HandleViewCalendar(Guid userId)
        {
            List<ToDoTask> task = this._taskService.GetCalendar(userId);

            this._view.PrintTaskTable(task);
        }

        private void DisplayTask(Guid userId, string userName)
        {
            this._view.PrintInfo($"Welcome {userName}");
            List<ToDoTask> tasks = this._taskService.GetRecentTask(userId);

            if (!tasks.Any())
            {
                this._view.PrintInfo("Add new tasks to continue ..");
                return;
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                if (i == 2)
                {
                    break;
                }

                this._view.PrintTask(tasks[i]);
            }
        }
    }
}
