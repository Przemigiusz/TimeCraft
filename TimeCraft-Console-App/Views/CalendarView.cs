using Spectre.Console;
using TimeCraft_Console_App.Messages;
using TimeCraft_Console_App.Models;

namespace TimeCraft_Console_App.Views
{
    internal class CalendarView
    {
        public CNavigationResult displayCalendar(List<string> months, List<string> dayAbbreviations, List<Meeting> meetings, List<Models.Task> tasks)
        {
            Console.Clear();
            Interface_Elements.Calendar.Calendar calendar = new Interface_Elements.Calendar.Calendar(months, dayAbbreviations, meetings, tasks);
            calendar.render();
            return calendar.navigate();
        }
    }
}