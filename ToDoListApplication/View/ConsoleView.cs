using ToDoListApplication.Models;
using ToDoListApplication.Models.Enums;
using ToDoListApplication.Validators;

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
            string input = this.GetValidatedInput(
               message,
               Validator.IsValidName,
               $"Please enter a name only alphabets.");
            return input;
        }

        internal int GetOption(string message)
        {
            string name = GetString(message);
            int value;
            while (!int.TryParse(name, out value))
            {
                Console.Clear();
                Console.WriteLine("Please enter a valid input");
                name = GetString(message);
            }

            return value;
        }

        internal string GetPassword(string message)
        {
            string input = this.GetValidatedInput(
                message,
                Validator.IsValidPassword,
                $"Please enter a password with atleast 8 digits.");
            return input;
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

        internal string GetTitle(string v)
        {
            string input = this.GetValidatedInput(
                v,
                Validator.IsValidName,
                $"Please enter a title with only alphabets.");
            return input;
        }

        /// <summary>
        /// Gets decimal input
        /// </summary>
        /// <param name="prompt">Message to be displayed</param>
        /// <param name="optional">True if we want to perform edit operation</param>
        /// <returns>decimal input</returns>
        public string GetDescription(string prompt)
        {
            string input = this.GetValidatedInput(
                prompt,
                Validator.IsValidDescription,
                $"Please enter a valid description with more than 10 characters and less than 30.");
            return input;
        }

        internal DateTime GetTargetDate(string prompt)
        {
            string input = this.GetValidatedInput(
                prompt,
                Validator.IsValidDate,
                $"Please enter a valid date.");
            return DateTime.Parse(input);
        }

        internal Recurrence GetRecurrence(string v)
        {
            int option = this.GetOption("1. Daily\n2. weekly\n3. Monthly\n4. Yearly");
            while (!(option >= 1 && option <= 4))
            {
                this.PrintInfo("Please enter a valid integer");
                option = this.GetOption("1. Daily\n2. weekly\n3. Monthly\n4. Yearly");
            }

            return (Recurrence)option;
        }

        internal void PrintTaskTable(List<ToDoTask> tasks)
        {
            int i = 1;
            Console.WriteLine($"S.No, Title, Description, Date");
            foreach (ToDoTask task in tasks)
            {
                Console.WriteLine($"{i++}. {task.Title}, {task.Description}, {task.Date.ToShortDateString()}");
            }
        }

        private string GetValidatedInput(string prompt, Func<string, bool> isValidField, string errorMessage)
        {
            int tries = 3;
            string input = this.GetString(prompt);

            while (!isValidField(input))
            {
                if (tries == 1)
                {
                    throw new InvalidDataException("No attempt left, Please try again." + Environment.NewLine);
                }

                Console.WriteLine(errorMessage);
                Console.WriteLine($"Tries left: {--tries}\n");
                input = this.GetString(prompt);
            }

            return input;
        }

        internal int GetIndex(string v)
        {
            string input = GetString("Enter the index to select the task: ");

            int index;
            while (!int.TryParse(input, out index))
            {
                Console.WriteLine("Enter a valid integer");
                input = GetString("Enter the index to select the task: ");
            }
            return index - 1;
        }

        internal void PrintTask(ToDoTask task)
        {
            Console.WriteLine($"{task.Title}, {task.Description}, {task.Date.ToShortDateString()}");
        }
    }
}
