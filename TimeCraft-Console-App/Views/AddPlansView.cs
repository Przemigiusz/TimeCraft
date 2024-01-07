using TimeCraft_Console_App.Interface_Elements;
using SharedLibrary.Models;

namespace TimeCraft_Console_App.Views
{
    internal class AddPlansView
    {
        public IPlan displayNewPlanForm(DateTime chosenDate, List<string> kindsOfMeetings, List<string> priorities) {
            Console.Clear();
            NewPlanForm newPlanForm = new NewPlanForm();
            IPlan newPlan = newPlanForm.render(chosenDate, kindsOfMeetings, priorities);
            Console.Clear();
            return newPlan;
        }
    }
}
