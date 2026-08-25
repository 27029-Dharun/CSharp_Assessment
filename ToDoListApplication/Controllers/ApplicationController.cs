namespace ToDoListApplication.Controllers
{
    internal class ApplicationController
    {

        private Guid _userId;
        private string _userName;
        private readonly UserController _userController;
        private readonly TaskController _taskController;
        public ApplicationController(UserController userController, TaskController taskController)
        {
            _userController = userController;
            _taskController = taskController;
        }

        /// <summary>
        /// Starts the applications
        /// </summary>
        public void Start()
        {
            while (true)
            {
                (_userId, _userName) = this._userController.Authenticate();

                if (_userId == Guid.Empty)
                {
                    return;
                }

                this._taskController.Dashboard(_userId, _userName);
            }
        }
    }
}
