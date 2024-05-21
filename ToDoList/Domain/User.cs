namespace ToDoList.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<ToDoItem> ToDoItems { get; set; }
    }
}
