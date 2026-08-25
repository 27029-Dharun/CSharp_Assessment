using ToDoListApplication.Models;
using ToDoListApplication.Models.Enums;
using ToDoListApplication.Validators;

namespace ToDoListApplication.View
{
    /// <summary>
    /// Contains all the view operations
    /// </summary>
    internal class ConsoleView
    {
        //Get a valid string from the user
        internal string GetString(string message)
        {
            Console.Write(message);
            string input = (Console.ReadLine() ?? string.Empty).Trim();
            return input;
        }

        /// <summary>
        /// Get the name of the user
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        internal string GetName(string message)
        {
            string input = this.GetValidatedInput(
               message,
               Validator.IsValidName,
               $"Please enter a name only alphabets.");
            return input;
        }

        /// <summary>
        /// Get option 
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get the password
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        internal string GetPassword(string message)
        {
            string input = this.GetValidatedInput(
                message,
                Validator.IsValidPassword,
                $"Please enter a password with atleast 8 digits.");
            return input;
        }

        /// <summary>
        /// Prints the info in console
        /// </summary>
        /// <param name="message"></param>
        internal void PrintInfo(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Pause the console and clear
        /// </summary>
        internal void PauseAndContinue()
        {
            Console.WriteLine("Press a key to continue ...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Get the title of the task
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get the target date
        /// </summary>
        /// <param name="prompt">Prompt </param>
        /// <returns>The target date</returns>
        internal DateTime GetTargetDate(string prompt)
        {
            string input = this.GetValidatedInput(
                prompt,
                Validator.IsValidDate,
                $"Please enter a valid date.");
            return DateTime.Parse(input);
        }

        /// <summary>
        /// get the recurrence option
        /// </summary>
        /// <param name="v">Message to be printed</param>
        /// <returns>get frequency</returns>
        internal Recurrence GetRecurrence(string v)
        {
            int option = this.GetOption("1. Daily\n2. weekly\n3. Monthly\n4. Yearly");
            while (!(option >= 1 && option <= 4))
            {
                this.PrintInfo("Please enter a valid integer");
                option = this.GetOption("1. Daily\n2. weekly\n3. Monthly\n4. Yearly\nEnter the option: ");
            }

            return (Recurrence)option;
        }

        /// <summary>
        /// Prints the task table
        /// </summary>
        /// <param name="tasks"></param>
        internal void PrintTaskTable(List<ToDoTask> tasks)
        {
            int i = 1;
            Console.WriteLine($"S.No, Title, Description, Date");
            foreach (ToDoTask task in tasks)
            {
                Console.WriteLine($"{i++}. {task.Title}, {task.Description}, {task.Date.ToShortDateString()}");
            }
        }

        /// <summary>
        /// get the valid input from user
        /// </summary>
        /// <param name="prompt">Prompt</param>
        /// <param name="isValidField">Field to be validated</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>A valid string value</returns>
        /// <exception cref="InvalidDataException">When user exceeds the tries</exception>
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

        /// <summary>
        /// Get the index of the task
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Print the task for dashboard
        /// </summary>
        /// <param name="task">task to be printed</param>
        internal void PrintTask(ToDoTask task)
        {
            Console.WriteLine($"{task.Title}, {task.Description}, {task.Date.ToShortDateString()}");
        }
    }
}
