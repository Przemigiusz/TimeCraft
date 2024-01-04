using SharedLibrary.Models;
using TimeCraft_Console_App.Messages;

namespace TimeCraft_Console_App.Views
{
    internal class CalendarView
    {
        public CNavigationResult displayCalendar(List<string> months, List<string> dayAbbreviations, List<Meeting> meetings, List<SharedLibrary.Models.Task> tasks)
        {
            Console.Clear();
            Interface_Elements.Calendar.Calendar calendar = new Interface_Elements.Calendar.Calendar(months, dayAbbreviations, meetings, tasks);
            calendar.render();
            CNavigationResult result = calendar.navigate();
            Console.Clear();
            return result;
        }
    }
}