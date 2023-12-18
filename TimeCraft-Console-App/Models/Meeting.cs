namespace TimeCraft_Console_App.Models
{
    internal class Meeting : IPlan
    {
        private static int currentMeetingId = 0;
        private int meetingId = 0;
        private string kindOfMeeting;
        private string topic;
        private string meetingDate;
        private string meetingStartTime;
        private string meetingEndTime;
        public Meeting(string kindOfMeeting, string topic, string meetingDate, string meetingStartTime, string meetingEndTime)
        {
            this.meetingId = currentMeetingId;
            ++currentMeetingId;
            this.kindOfMeeting = kindOfMeeting;
            this.topic = topic;
            this.meetingDate = meetingDate;
            this.meetingStartTime = meetingStartTime;
            this.meetingEndTime = meetingEndTime;
        }
        public int MeetingId { get { return meetingId; } set { meetingId = value; } }
        public string KindOfMeeting { get { return kindOfMeeting; } set { kindOfMeeting = value; } }
        public string Topic { get { return topic; } set { topic = value; } }
        public string MeetingDate { get {  return meetingDate; } set { meetingDate = value; } }
        public string MeetingStartTime { get {  return meetingStartTime; } set { meetingStartTime = value; } }
        public string MeetingEndTime { get {  return meetingEndTime; } set { meetingEndTime = value; } }
    }
}
