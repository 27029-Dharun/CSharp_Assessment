using ToDoListApplication.Models;

namespace ToDoListApplication.Repository
{
    internal class UserRepository
    {
        private readonly string _userFilePath;
        private readonly List<User> _users;
        public UserRepository(string path)
        {
            _userFilePath = path;
            _users = JsonHelper.ReadAll<User>(_userFilePath);
        }

        public void Add(User user)
        {
            _users.Add(user);
            JsonHelper.WriteAll(_userFilePath, _users);
        }

        public User? GetByUserName(string userName)
        {
            return _users.FirstOrDefault(x => x.UserName == userName);
        }

        internal List<string> GetByUserName()
        {
            return _users.Select(x => x.UserName).ToList();
        }
    }
}
