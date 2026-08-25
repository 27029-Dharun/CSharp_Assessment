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
    }
}
