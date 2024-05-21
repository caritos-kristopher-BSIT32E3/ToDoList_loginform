namespace ToDoList.Domain
{
    public class ProgressReport
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public double CompletionPercentage { get; set; }
    }
}
