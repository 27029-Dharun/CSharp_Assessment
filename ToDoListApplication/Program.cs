using ToDoListApplication.Controllers;
using ToDoListApplication.Repository;
using ToDoListApplication.Services;
using ToDoListApplication.View;

namespace ToDoListApplication
{
    /// <summary>
    /// Program class for starting the operation.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point of the application wires up the dependencies once.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ConsoleView view = new ConsoleView();
            UserRepository userRepository = new UserRepository("User.json");
            UserService userService = new UserService(userRepository);
            UserController userController = new UserController(userService, view);

            TaskRepository taskRepository = new TaskRepository("Task.json");
            TaskService taskService = new TaskService(taskRepository);
            TaskController taskController = new TaskController(taskService, view);

            ApplicationController controller = new ApplicationController(userController, taskController);
            controller.Start();
        }
    }
}
