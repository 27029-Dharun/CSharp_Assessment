namespace ToDoListApplication.Models
{
    /// <summary>
    /// Represent an user 
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes the User object
        /// </summary>
        /// <param name="name">Name of the user</param>
        /// <param name="password">Password</param>
        public User(Guid id, string name, string password)
        {
            Id = id;
            UserName = name;
            Password = password;
        }

        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// User name 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
