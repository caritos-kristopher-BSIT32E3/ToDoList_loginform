namespace ToDoList.Models
{
    public class ToDoItemViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        // Add any other properties as needed for creating a new task

        // Constructor if needed
        public ToDoItemViewModel()
        {
        }
    }
}
