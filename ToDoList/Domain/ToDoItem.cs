using System;

namespace ToDoList.Domain
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }

        // Add UserId property to associate the to-do item with a specific user
        public int UserId { get; set; }
        public DateTime? CompletedDate { get; set; }
        // Add any other properties as needed
    }
}
