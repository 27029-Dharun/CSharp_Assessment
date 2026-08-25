using System.Text.Json;
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
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        /// <summary>
        /// Initialize the object
        /// </summary>
        /// <param name="path">The file path where the data is stored</param>
        public UserRepository(string path)
        {
            _userFilePath = path;
            _users = LoadAll(_userFilePath);
        }

        /// <summary>
        /// Writes all the task to the file
        /// </summary>
        /// <param name="filePath">The path of the file where the task are stored</param>
        /// <param name="list">List of the tasks that are to be added</param>
        public static void WriteAll(string filePath, List<User> list)
        {
            string json = JsonSerializer.Serialize(list, options);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Loads all the content and loads into the file
        /// </summary>
        /// <param name="filePath">Path of the file from which the contents are loaded </param>
        /// <returns>A list of tasks that are stored in the file</returns>
        public static List<User> LoadAll(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
                return new List<User>();
            }

            string text = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(text))
            {
                return new List<User>();
            }

            List<User> tasks = JsonSerializer.Deserialize<List<User>>(text, options) ?? new List<User>(); ;

            return tasks;
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="user"></param>
        public void Add(User user)
        {
            _users.Add(user);
            WriteAll(_userFilePath, _users);
        }

        /// <summary>
        /// Get the user by Id
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>A user object with the user name</returns>
        public User? GetByUserName(string userName)
        {
            return Copy(_users.FirstOrDefault(x => x.UserName == userName));
        }

        private User? Copy(User? user)
        {
            if (user is null)
            {
                return null;
            }

            return new User(user.Id, user.UserName, user.Password);
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
