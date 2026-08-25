using ToDoListApplication.Controllers;
using ToDoListApplication.Repository;
using ToDoListApplication.Services;
using ToDoListApplication.View;

namespace ToDoListApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleView view = new ConsoleView();
            UserRepository userRepository = new UserRepository("User.json");
            UserService userService = new UserService(userRepository);
            UserController userController = new UserController(userService, view);

            ApplicationController controller = new ApplicationController(userController);
            controller.Start();
        }
    }
}
