using Spectre.Console;
using TimeCraft_Console_App.Interface_Elements;
using TimeCraft_Console_App.Models;

namespace TimeCraft_Console_App.Views
{
    internal class AddPlansView
    {
        public IPlan displayNewPlanForm(DateTime chosenDate) {
            Console.Clear();
            NewPlanForm newPlanForm = new NewPlanForm();
            IPlan newPlan = newPlanForm.render(chosenDate);
            Console.Clear();
            return newPlan;
        }
    }
}
