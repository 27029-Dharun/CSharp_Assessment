namespace ToDoListApplication.Models.Enums
{
    /// <summary>
    /// Specifies the options available in the dashboard
    /// </summary>
    internal enum DashboardOption
    {
        /// <summary>
        /// Represents an option to add new task
        /// </summary>
        Add = 1,

        /// <summary>
        /// Represents an option to view all task
        /// </summary>
        View = 2,

        /// <summary>
        /// Represents an option to remove task
        /// </summary>
        Remove = 3,

        /// <summary>
        /// Represents an option to update task
        /// </summary>
        Update = 4,

        /// <summary>
        /// Represents an option to view calendar
        /// </summary>
        Calendar = 5,

        /// <summary>
        /// Represents an option to logout application.
        /// </summary>
        LogOut = 6,
    }
}
