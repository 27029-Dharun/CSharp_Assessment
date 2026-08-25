namespace ToDoListApplication.Controllers
{
    internal class ApplicationController
    {
        private readonly UserController _userController;
        private Guid _userId;
        public ApplicationController(UserController controller)
        {
            _userController = controller;
        }

        /// <summary>
        /// Starts the applications
        /// </summary>
        public void Start()
        {
            while (true)
            {
                _userId = this._userController.Authenticate();

                if (_userId == Guid.Empty)
                {
                    return;
                }

                Console.WriteLine("Logged in successfully");
            }
        }
    }
}
