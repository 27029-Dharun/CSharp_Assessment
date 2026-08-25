using ToDoListApplication.Models.Enums;
using ToDoListApplication.Services;
using ToDoListApplication.View;

namespace ToDoListApplication.Controllers
{
    internal class UserController
    {
        private readonly UserService _userService;
        private readonly ConsoleView _view;
        public UserController(UserService service, ConsoleView view)
        {
            _userService = service ?? throw new ArgumentNullException();
            _view = view ?? throw new ArgumentNullException();
        }

        public Guid Authenticate()
        {
            while (true)
            {
                AuthenticationOption option = (AuthenticationOption)this._view.GetOption("1. Sign Up\n2. Log In\n3. Exit\n");
                switch (option)
                {
                    case AuthenticationOption.SignUp:
                        this.HandleSignUp();
                        break;

                    case AuthenticationOption.LogIn:
                        Guid id = this.HandleLogIn();
                        Console.WriteLine(id);
                        if (id != Guid.Empty)
                        {
                            return id;
                        }
                        break;

                    case AuthenticationOption.Exit:
                        return Guid.Empty;
                }
                this._view.PauseAndContinue();
            }
        }

        private void HandleSignUp()
        {
            string name = this._view.GetName("Enter the name of the user: ");
            string password = this._view.GetPassword("Enter the password for the account: ");
            string confirmPassword = this._view.GetPassword("Enter the password again to confirm: ");

            if (this._userService.AddUser(name, password))
            {
                this._view.PrintInfo("Account created successfully");
                return;
            }

            this._view.PrintInfo("Failed to create an account");
        }

        private Guid HandleLogIn()
        {
            string name = this._view.GetName("Enter the name of the user: ");
            string password = this._view.GetPassword("Enter the password for the account: ");

            return this._userService.ValidateLogIn(name, password);
        }
    }
}
