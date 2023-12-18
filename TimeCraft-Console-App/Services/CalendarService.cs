using System.Collections.Generic;
using System.Globalization;

namespace TimeCraft_Console_App.Services
{
    internal class CalendarService
    {
        private static CalendarService? instance;
        private CalendarService() { }

        public static CalendarService Instance
        {
            get
            {
                instance ??= new CalendarService();
                return instance;
            }
        }

        private List<DateTime> getCalendarDaysBaseOnMonthAndYear(int year, int month)
        {
            List<DateTime> daysList = new List<DateTime>();
            int daysInMonth = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(year, month, day);
                daysList.Add(currentDate);
            }
            return daysList;
        }

        public List<DateTime> getAllDaysInYear(int year)
        {
            List<DateTime> allDaysList = new List<DateTime>();

            for (int month = 1; month <= 12; month++)
            {
                List<DateTime> daysList = getCalendarDaysBaseOnMonthAndYear(year, month);
                allDaysList.AddRange(daysList);
            }

            return allDaysList;
        }

        public DateTime getCurrentDate()
        {
            return DateTime.Now;
        }

        public List<string> getDayAbbreviations()
        {
            List<string> dayAbbreviations = new List<string>
            {
                "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"
            };
            return dayAbbreviations;
        }

        public List<string> getMonths()
        {
            List<string> months = new List<string>();

            for (int month = 1; month <= 12; month++)
            {
                string monthName = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(month);
                months.Add(monthName);
            }

            return months;
        }
    }
}
