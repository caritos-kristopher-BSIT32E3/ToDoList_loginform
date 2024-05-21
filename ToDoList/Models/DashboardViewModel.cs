using System.Collections.Generic;
using ToDoList.Domain;

namespace ToDoList.Models
{
    public class DashboardViewModel
    {
        // Property to hold all to-do items (you already have this)
        public List<ToDoItem> ToDoItems { get; set; }

        // New property to hold pending to-do items
        public List<ToDoItem> PendingToDoItems { get; set; }

        // New property to hold completed to-do items (optional, if used in your application)
        public List<ToDoItem> DoneToDoItems { get; set; }

        // Additional properties for the dashboard view model
        public string Username { get; set; }  // Username of the current user
        public int PendingCount { get; set; } // Number of pending tasks
        public int DoneCount { get; set; }    // Number of completed tasks
    }
}
