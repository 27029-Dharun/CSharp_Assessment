using ToDoListApplication.Models;
using ToDoListApplication.Repository;
using ToDoListApplication.Validators;

namespace ToDoListApplication.Services
{
    /// <summary>
    /// user service
    /// </summary>
    internal class UserService
    {

        private readonly UserRepository _userRepository;

        /// <summary>
        /// Initialize the object 
        /// </summary>
        /// <param name="repository">Instance of repository</param>
        public UserService(UserRepository repository)
        {
            _userRepository = repository;
        }

        /// <summary>
        /// Add the new user
        /// </summary>
        /// <param name="username">user name</param>
        /// <param name="password">Password</param>
        /// <returns>A boolean true if add; otherwise false</returns>
        public bool AddUser(string username, string password)
        {
            List<string> userList = this._userRepository.GetUserName();
            if (!Validator.IsUniqueField(username, userList))
            {
                return false;
            }

            User user = new User(Guid.NewGuid(), username, password);
            this._userRepository.Add(user);
            return true;
        }

        /// <summary>
        /// Validates the login
        /// </summary>
        /// <param name="username">user name</param>
        /// <param name="password">Password</param>
        /// <returns>Guid of the user if logged in</returns>
        public (Guid, string) ValidateLogIn(string username, string password)
        {
            User? user = this._userRepository.GetByUserName(username);
            if (user is null)
            {
                return (Guid.Empty, string.Empty);
            }

            if (user.Password != password)
            {
                return (Guid.Empty, string.Empty);
            }

            return (user.Id, user.UserName);
        }
    }
}
