
namespace ToDoListApplication.View
{
    internal class ConsoleView
    {
        internal string GetString(string message)
        {
            Console.Write(message);
            string input = (Console.ReadLine() ?? string.Empty).Trim();
            return input;
        }

        internal string GetName(string message)
        {
            string name = GetString(message);
            return name;
        }

        internal int GetOption(string message)
        {
            string name = GetString(message);
            int value;
            while (!int.TryParse(name, out value))
            {
                Console.WriteLine("Please enter a valid input");
                name = GetString(message);
            }

            return value;
        }

        internal string GetPassword(string message)
        {
            string name = GetString(message);
            return name;
        }

        internal void PrintInfo(string message)
        {
            Console.WriteLine(message);
        }

        internal void PauseAndContinue()
        {
            Console.WriteLine("Press a key to continue ...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
