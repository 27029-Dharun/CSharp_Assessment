using ToDoListApplication.Models;

namespace ToDoListApplication.Repository
{
    /// <summary>
    /// Stores the user details in the repository
    /// </summary>
    internal class UserRepository
    {
        private readonly string _userFilePath;
        private readonly List<User> _users;

        /// <summary>
        /// Initialize the object
        /// </summary>
        /// <param name="path">The file path where the data is stored</param>
        public UserRepository(string path)
        {
            _userFilePath = path;
            _users = JsonHelper.ReadAll<User>(_userFilePath);
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="user"></param>
        public void Add(User user)
        {
            _users.Add(user);
            JsonHelper.WriteAll(_userFilePath, _users);
        }

        /// <summary>
        /// Get the user by Id
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>A user object with the user name</returns>
        public User? GetByUserName(string userName)
        {
            return _users.FirstOrDefault(x => x.UserName == userName);
        }

        /// <summary>
        /// Gets all the users.
        /// </summary>
        /// <returns>A list of user names</returns>
        internal List<string> GetUserName()
        {
            return _users.Select(x => x.UserName).ToList();
        }
    }
}
