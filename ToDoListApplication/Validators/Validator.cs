namespace ToDoListApplication.Validators
{
    internal class Validator
    {
        internal static bool IsUniqueField(string username, List<string> userList)
        {
            foreach (string existingUser in userList)
            {
                if (existingUser == username)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
