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
                try
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
            if (this._taskService.GetTaskExist(userId))
            {
                this._view.PrintInfo("No task available to update");
                return;
            }

            List<ToDoTask> tasks = this._taskService.ViewUserTask(userId);

            this._view.PrintTasks(tasks);

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
                        task.Date = this._view.GetTargetDate("Enter the date");
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
            if (this._taskService.GetTaskExist(userId))
            {
                this._view.PrintInfo("No task available to remove");
                return;
            }

            List<ToDoTask> tasks = this._taskService.ViewUserTask(userId);

            this._view.PrintTasks(tasks);

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
            List<ToDoTask> task = this._taskService.ViewUserTask(userId);

            this._view.PrintTasks(task);
        }

        private void DisplayTask(Guid userId, string userName)
        {
            this._view.PrintInfo($"Welcome {userName}");
        }
    }
}
