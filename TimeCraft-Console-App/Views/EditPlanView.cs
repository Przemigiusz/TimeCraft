using TimeCraft_Console_App.Interface_Elements.Forms;
using SharedLibrary.Models;

namespace TimeCraft_Console_App.Views
{
    internal class EditPlanView
    {
        public IPlan? displayEditPlanForm(IPlan? plan, List<string> kindsOfMeetings, List<string> priorities)
        {
            Console.Clear();
            EditPlanForm editPlanForm = new EditPlanForm();
            IPlan? editedPlan = editPlanForm.render(plan, kindsOfMeetings, priorities);
            Console.Clear();
            return editedPlan;
        }
    }
}
