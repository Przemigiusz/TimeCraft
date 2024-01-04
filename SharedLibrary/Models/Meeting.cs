using System.Globalization;

namespace SharedLibrary.Models
{
    public class Meeting : IPlan
    {
        private static int currentMeetingId = 0;
        private int meetingId = 0;
        private string kindOfMeeting;
        private string topic;
        private string meetingDate;
        private string meetingStartTime;
        private string meetingEndTime;
        private DateTime meetingStartTimeDate;
        private DateTime meetingEndTimeDate;
        private int userId;

        public Meeting(string kindOfMeeting, string topic, string meetingDate, string meetingStartTime, string meetingEndTime, int userId)
        {
            this.meetingId = currentMeetingId;
            ++currentMeetingId;
            this.kindOfMeeting = kindOfMeeting;
            this.topic = topic;
            this.meetingDate = meetingDate;
            this.meetingStartTime = meetingStartTime;
            this.meetingEndTime = meetingEndTime;
            this.userId = userId;

            if (DateTime.TryParseExact($"{meetingDate} {meetingStartTime}", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDateTime))
            {
                this.meetingStartTimeDate = startDateTime;
            }

            if (DateTime.TryParseExact($"{meetingDate} {meetingEndTime}", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDateTime))
            {
                this.meetingEndTimeDate = endDateTime;
            }
        }
        public int MeetingId { get { return meetingId; } set { meetingId = value; } }
        public string KindOfMeeting { get { return kindOfMeeting; } set { kindOfMeeting = value; } }
        public string Topic { get { return topic; } set { topic = value; } }
        public string MeetingDate { get { return meetingDate; } set { meetingDate = value; } }
        public string MeetingStartTime { get { return meetingStartTime; } set { meetingStartTime = value; } }
        public string MeetingEndTime { get { return meetingEndTime; } set { meetingEndTime = value; } }
        public int UserId { get { return userId; } set { userId = value; } }
        public DateTime MeetingStartTimeDate
        {
            get { return meetingStartTimeDate; }
            set
            {
                if (meetingStartTimeDate != value)
                {
                    if (value != DateTime.MinValue)
                    {
                        meetingStartTimeDate = value;
                        meetingStartTime = meetingStartTimeDate.ToString("HH:mm");
                    }
                }
            }
        }
        public DateTime MeetingEndTimeDate
        {
            get { return meetingEndTimeDate; }
            set
            {
                if (meetingEndTimeDate != value)
                {
                    if (value != DateTime.MinValue)
                    {
                        meetingEndTimeDate = value;
                        meetingEndTime = meetingEndTimeDate.ToString("HH:mm");
                    }
                }
            }
        }
    }
}
