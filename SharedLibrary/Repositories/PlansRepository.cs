using SharedLibrary.Models;
using System.Globalization;

namespace SharedLibrary.Repositories
{
    public class PlansRepository
    {
        private static PlansRepository? instance;

        private List<string> kindsOfMeetings;
        private List<string> priorities;
        private List<Models.Task> tasks;
        private List<Meeting> meetings;

        public List<string> KindsOfMeetings { get { return kindsOfMeetings; } }
        public List<string> Priorities { get { return priorities; } }
        public List<Meeting> Meetings { get { return meetings; } }
        public List<Models.Task> Tasks { get { return tasks; } }

        public static PlansRepository Instance
        {
            get
            {
                instance ??= new PlansRepository();
                return instance;
            }
        }

        private PlansRepository()
        {
            this.kindsOfMeetings = new List<string>();
            this.priorities = new List<string>();

            this.tasks = new List<Models.Task>();
            this.meetings = new List<Meeting>();

            this.initializePlans();
            this.initializePlansComponents();
        }

        private void initializePlans()
        {
            Meeting meeting1 = new Meeting("Conference", "Testing", "13/11/2023", "12:00", "13:00", 0);
            Meeting meeting2 = new Meeting("Team Meeting", "Debugging", "13/11/2023", "14:00", "15:00", 0);
            Meeting meeting3 = new Meeting("Conference", "New DB", "13/11/2023", "16:00", "17:00", 0);

            Meeting meeting4 = new Meeting("Team Meeting", "New Concepts", "12/11/2023", "11:00", "12:00", 1);
            Meeting meeting5 = new Meeting("Client Meeting", "Changes", "11/11/2023", "10:00", "11:00", 1);

            Meeting meeting6 = new Meeting("Conference", "Testing2", "13/11/2023", "12:00", "13:00", 2);
            Meeting meeting7 = new Meeting("Team Meeting", "Debugging2", "13/11/2023", "14:00", "15:00", 2);
            Meeting meeting8 = new Meeting("Conference", "New DB2", "13/11/2023", "16:00", "17:00", 2);
            Meeting meeting9 = new Meeting("Conference", "Testing3", "13/11/2023", "17:30", "18:00", 2);

            Meeting meeting10 = new Meeting("Conference", "New DB5", "04/01/2024", "16:00", "17:00", 0);
            Meeting meeting11 = new Meeting("Conference", "Testing5", "04/01/2024", "17:30", "18:00", 1);

            Models.Task task1 = new Models.Task("Refuel the car", "My mum wants me to refuel her car", "13/11/2023", "Low", false, 0);
            Models.Task task2 = new Models.Task("Respond to emails", "Respond to my teachers emails", "13/11/2023", "Normal", false, 0);
            Models.Task task3 = new Models.Task("Buy gift", "Buy gift for Magda's birthday", "13/11/2023", "High", false, 0);

            Models.Task task4 = new Models.Task("Shopping", "Buy new t-shirt for my boxing lessons", "12/11/2023", "Normal", false, 1);
            Models.Task task5 = new Models.Task("New haircut", "Visit Elegancko BarberShop", "11/11/2023", "High", false, 1);
            Models.Task task6 = new Models.Task("Refuel the car2", "My mum wants me to refuel her car", "13/11/2023", "Low", false, 1);

            Models.Task task7 = new Models.Task("Respond to emails2", "Respond to my teachers emails", "13/11/2023", "Normal", false, 2);
            Models.Task task8 = new Models.Task("Buy gift2", "Buy gift for Magda's birthday", "13/11/2023", "High", false, 2);
            Models.Task task9 = new Models.Task("Refuel the car3", "My mum wants me to refuel her car", "13/11/2023", "Low", false, 2);

            Models.Task task10 = new Models.Task("Buy gift2", "Buy gift for Magda's birthday", "04/01/2024", "High", false, 0);
            Models.Task task11 = new Models.Task("Refuel the car3", "My mum wants me to refuel her car", "04/01/2024", "Low", false, 1);

            this.tasks.Add(task1);
            this.tasks.Add(task2);
            this.tasks.Add(task3);
            this.tasks.Add(task4);
            this.tasks.Add(task5);

            this.tasks.Add(task6);
            this.tasks.Add(task7);
            this.tasks.Add(task8);
            this.tasks.Add(task9);

            this.tasks.Add(task10);
            this.tasks.Add(task11);

            this.meetings.Add(meeting1);
            this.meetings.Add(meeting2);
            this.meetings.Add(meeting3);
            this.meetings.Add(meeting4);
            this.meetings.Add(meeting5);

            this.meetings.Add(meeting6);
            this.meetings.Add(meeting7);
            this.meetings.Add(meeting8);
            this.meetings.Add(meeting9);

            this.meetings.Add(meeting10);
            this.meetings.Add(meeting11);
        }

        private void initializePlansComponents()
        {
            this.kindsOfMeetings.Add("Conference");
            this.kindsOfMeetings.Add("Team Meeting");
            this.kindsOfMeetings.Add("Client Meeting");

            this.priorities.Add("Low");
            this.priorities.Add("Normal");
            this.priorities.Add("High");
        }

        public void addTask(Models.Task task)
        {
            this.tasks.Add(task);
        }
        public void addMeeting(Meeting meeting)
        {
            this.meetings.Add(meeting);
        }
        public void updateTask(Models.Task task)
        {
            Models.Task? taskToUpdate = tasks.FirstOrDefault(t => t.TaskId == task.TaskId);
            if (taskToUpdate != null)
            {
                taskToUpdate.TaskName = task.TaskName;
                taskToUpdate.TaskDescription = task.TaskDescription;
                taskToUpdate.TaskDate = task.TaskDate;
                taskToUpdate.TaskPriority = task.TaskPriority;
                taskToUpdate.IsCompleted = task.IsCompleted;
            }
        }
        public void updateMeeting(Meeting meeting)
        {
            Meeting? meetingToUpdate = meetings.FirstOrDefault(m => m.MeetingId == meeting.MeetingId);
            if (meetingToUpdate != null)
            {
                meetingToUpdate.KindOfMeeting = meeting.KindOfMeeting;
                meetingToUpdate.Topic = meeting.Topic;
                meetingToUpdate.MeetingDate = meeting.MeetingDate;
                meetingToUpdate.MeetingStartTime = meeting.MeetingStartTime;
                meetingToUpdate.MeetingEndTime = meeting.MeetingEndTime;
            }
        }

        public List<Meeting> getMeetings(DateTime chosenDate)
        {
            string formattedDate = chosenDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            return meetings.Where(meeting =>
                meeting.MeetingDate == formattedDate)
                .ToList();
        }
        public List<Models.Task> getTasks(DateTime chosenDate)
        {
            string formattedDate = chosenDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            return tasks.Where(task =>
                task.TaskDate == formattedDate)
                .ToList();
        }

        public List<Meeting> getMeetingsById(int userId, DateTime chosenDate)
        {
            string formattedDate = chosenDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            return meetings.Where(meeting =>
                meeting.MeetingDate == formattedDate && meeting.UserId == userId)
                .ToList();
        }
        public List<Models.Task> getTasksById(int userId, DateTime chosenDate)
        {
            string formattedDate = chosenDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            return tasks.Where(task =>
                task.TaskDate == formattedDate && task.UserId == userId)
                .ToList();
        }

        public List<Meeting> getAllMeetingsById(int userId)
        {
            return meetings.Where(meeting => meeting.UserId == userId)
                .ToList();
        }
        public List<Models.Task> getAllTasksById(int userId)
        {
            return tasks.Where(task => task.UserId == userId)
                .ToList();
        }

        public Meeting? getMeeting(int planId)
        {
            Meeting? meetingToGet = meetings.FirstOrDefault(meeting => meeting.MeetingId == planId);
            return meetingToGet;
        }
        public Models.Task? getTask(int planId)
        {
            Models.Task? taskToGet = tasks.FirstOrDefault(task => task.TaskId == planId);
            return taskToGet;
        }
        public void deleteMeeting(int meetingId)
        {
            Meeting? meetingToRemove = meetings.FirstOrDefault(meeting => meeting.MeetingId == meetingId);
            if (meetingToRemove != null)
            {
                meetings.Remove(meetingToRemove);
            }
        }
        public void deleteTask(int taskId)
        {
            Models.Task? taskToRemove = tasks.FirstOrDefault(task => task.TaskId == taskId);
            if (taskToRemove != null)
            {
                tasks.Remove(taskToRemove);
            }
            Console.WriteLine("test");
        }

    }
}
