
using System.Globalization;

namespace ToDoListApplication.Validators
{
    internal class Validator
    {
        /// <summary>
        /// Checks if the field is unique.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="fieldList"></param>
        /// <returns></returns>
        internal static bool IsUniqueField(string field, List<string> fieldList)
        {
            foreach (string existing in fieldList)
            {
                if (existing == field)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        internal static bool IsValidDescription(string arg)
        {
            return true;
        }

        /// <summary>
        /// Validates the date used in the transaction
        /// </summary>
        /// <param name="date">Date of the transaction</param>
        /// <returns>A string containing the validation output; empty string if it is valid. </returns>
        public static bool IsValidDate(string date)
        {
            if (!DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validDate))
            {
                return false;
            }

            if (validDate < DateTime.Today)
            {
                return false;
            }

            return true;
        }
    }
}
