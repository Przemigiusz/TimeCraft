using TimeCraft_Console_App.Interface_Elements.PlansTable;
using TimeCraft_Console_App.Messages;
using SharedLibrary.Models;

namespace TimeCraft_Console_App.Views
{
    internal class PlansView
    {
        public DSNavigationResult displayPlans(List<Meeting> meetings, List<SharedLibrary.Models.Task> tasks) {
            Console.Clear();
            DaySchedule daySchedule = new DaySchedule(meetings, tasks);
            daySchedule.render();
            DSNavigationResult result = daySchedule.navigate();
            Console.Clear();
            return result;
        }    
    }
}
