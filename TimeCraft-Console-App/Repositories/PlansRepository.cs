﻿using System.Globalization;
using TimeCraft_Console_App.Models;

namespace TimeCraft_Console_App.Repositories
{
    internal class PlansRepository
    {
        private List<Models.Task> tasks;
        private List<Meeting> meetings;
        public List<Meeting> Meetings { get { return meetings; } }
        public List<Models.Task> Tasks { get { return tasks; } }

        public PlansRepository() {
            this.tasks = new List<Models.Task>();
            this.meetings = new List<Meeting>();
            this.initializePlans();
        }
        private void initializePlans() {
            Meeting meeting1 = new Meeting("Conference", "Testing", "13/11/2023", "12:00", "13:00");
            Meeting meeting2 = new Meeting("Team Meeting", "Debugging", "13/11/2023", "14:00", "15:00");
            Meeting meeting3 = new Meeting("Conference", "New DB", "13/11/2023", "16:00", "17:00");

            Meeting meeting4 = new Meeting("Team Meeting", "New Concepts", "12/11/2023", "11:00", "12:00");
            Meeting meeting5 = new Meeting("Client Meeting", "Changes", "11/11/2023", "10:00", "11:00");

            Meeting meeting6 = new Meeting("Conference", "Testing2", "13/11/2023", "12:00", "13:00");
            Meeting meeting7 = new Meeting("Team Meeting", "Debugging2", "13/11/2023", "14:00", "15:00");
            Meeting meeting8 = new Meeting("Conference", "New DB2", "13/11/2023", "16:00", "17:00");
            Meeting meeting9 = new Meeting("Conference", "Testing3", "13/11/2023", "17:30", "18:00");

            Models.Task task1 = new Models.Task("Refuel the car", "My mum wants me to refuel her car", "13/11/2023", "Low", false);
            Models.Task task2 = new Models.Task("Respond to emails", "Respond to my teachers emails", "13/11/2023", "Normal", false);
            Models.Task task3 = new Models.Task("Buy gift", "Buy gift for Magda's birthday", "13/11/2023", "High", false);
            Models.Task task4 = new Models.Task("Shopping", "Buy new t-shirt for my boxing lessons","12/11/2023", "Normal", false);
            Models.Task task5 = new Models.Task("New haircut", "Visit Elegancko BarberShop", "11/11/2023", "High", false);

            Models.Task task6 = new Models.Task("Refuel the car2", "My mum wants me to refuel her car", "13/11/2023", "Low", false);
            Models.Task task7 = new Models.Task("Respond to emails2", "Respond to my teachers emails", "13/11/2023", "Normal", false);
            Models.Task task8 = new Models.Task("Buy gift2", "Buy gift for Magda's birthday", "13/11/2023", "High", false);
            Models.Task task9 = new Models.Task("Refuel the car3", "My mum wants me to refuel her car", "13/11/2023", "Low", false);

            this.tasks.Add(task1);
            this.tasks.Add(task2);
            this.tasks.Add(task3);
            this.tasks.Add(task4);
            this.tasks.Add(task5);

            this.tasks.Add(task6);
            this.tasks.Add(task7);
            this.tasks.Add(task8);
            this.tasks.Add(task9);

            this.meetings.Add(meeting1);
            this.meetings.Add(meeting2);
            this.meetings.Add(meeting3);
            this.meetings.Add(meeting4);
            this.meetings.Add(meeting5);

            this.meetings.Add(meeting6);
            this.meetings.Add(meeting7);
            this.meetings.Add(meeting8);
            this.meetings.Add(meeting9);
        }
        public void addTask(Models.Task task) {
            this.tasks.Add(task);
        }
        public void addMeeting(Meeting meeting) {
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
