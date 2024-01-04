namespace SharedLibrary.Models
{
    public class Task : IPlan
    {
        private static int currentTaskId = 0;
        private int taskId = 0;
        private string taskName;
        private string taskDescription;
        private string taskDate;
        private string taskPriority;
        private bool isCompleted;
        private int userId;

        public Task(string taskName, string taskDescription, string taskDate, string taskPriority, bool isCompleted, int userId)
        {
            this.taskId = currentTaskId;
            ++currentTaskId;
            this.taskName = taskName;
            this.taskDescription = taskDescription;
            this.taskDate = taskDate;
            this.taskPriority = taskPriority;
            this.isCompleted = isCompleted;
            this.userId = userId;
        }
        public int TaskId { get { return taskId; } set { taskId = value; } }
        public string TaskName { get { return taskName; } set { taskName = value; } }
        public string TaskDescription { get { return taskDescription; } set { taskDescription = value; } }
        public string TaskDate { get { return taskDate; } set { taskDate = value; } }
        public string TaskPriority { get { return taskPriority; } set { taskPriority = value; } }
        public bool IsCompleted { get { return isCompleted; } set { isCompleted = value; } }
        public int UserId { get { return userId; } set { userId = value; } }
    }
}
