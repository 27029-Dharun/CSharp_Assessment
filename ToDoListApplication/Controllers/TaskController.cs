using ToDoListApplication.Models;
using ToDoListApplication.Models.Enums;
using ToDoListApplication.Services;
using ToDoListApplication.View;

namespace ToDoListApplication.Controllers
{
    internal class TaskController
    {
        private readonly TaskService _taskService;
        private readonly ConsoleView _view;

        public TaskController(TaskService service, ConsoleView view)
        {
            this._view = view;
            this._taskService = service;
        }

        public Guid Dashboard(Guid userId, string userName)
        {
            while (true)
            {
                this.DisplayTask(userId, userName);

                DashboardOption option = (DashboardOption)this._view.GetOption("1. Add new task\n" +
                    "2. View all task\n" +
                    "3. Remove existing task\n" +
                    "4. Edit existing task\n" +
                    "5. View calendar\n");

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
                        return Guid.Empty;

                    default:
                        this._view.PrintInfo("Enter an valid option");
                        break;
                }
            }
        }

        private void HandleAddTask(Guid userId)
        {
            string title = this._view.GetTitle("Enter the title of the task: ");
            string description = this._view.GetDescription("Enter the description: ");
            DateTime date = this._view.GetTargetDate("Enter the date: ");
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
            if (this._taskService.GetTaskExist(userId))
            {
                this._view.PrintInfo("No task available to view");
                return;
            }

            List<ToDoTask> tasks = this._taskService.ViewUserTask(userId);

            this._view.PrintTasks(tasks);
        }

        private void HandleUpdateTask(Guid userId)
        {
            throw new NotImplementedException();
        }

        private void HandleRemoveTask(Guid userId)
        {
            throw new NotImplementedException();
        }

        private void HandleViewCalendar(Guid userId)
        {
            throw new NotImplementedException();
        }

        private void DisplayTask(Guid userId, string userName)
        {
            this._view.PrintInfo("Dashboard");
        }
    }
}
