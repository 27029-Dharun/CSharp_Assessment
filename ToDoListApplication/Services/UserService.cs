using ToDoListApplication.Models;
using ToDoListApplication.Repository;
using ToDoListApplication.Validators;

namespace ToDoListApplication.Services
{
    internal class UserService
    {

        private readonly UserRepository _userRepository;

        public UserService(UserRepository repository)
        {
            _userRepository = repository;
        }

        public bool AddUser(string username, string password)
        {
            List<string> userList = this._userRepository.GetByUserName();
            if (!Validator.IsUniqueField(username, userList))
            {
                return false;
            }

            User user = new User(Guid.NewGuid(), username, password);
            this._userRepository.Add(user);
            return true;
        }

        public Guid ValidateLogIn(string username, string password)
        {
            User? user = this._userRepository.GetByUserName(username);
            if (user is null)
            {
                return Guid.Empty;
            }

            if (user.Password != password)
            {
                return Guid.Empty;
            }

            return user.Id;
        }
    }
}
