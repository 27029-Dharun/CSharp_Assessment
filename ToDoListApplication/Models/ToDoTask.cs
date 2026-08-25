namespace ToDoListApplication.Models
{
    internal class ToDoTask
    {
        public ToDoTask()
        {

        }

        public ToDoTask(Guid guid, Guid userId, string title, string description, DateTime taskDate)
        {
            Id = guid;
            UserId = userId;
            Title = title;
            Description = description;
            Date = taskDate;
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
