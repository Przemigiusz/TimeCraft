using Spectre.Console;
using TimeCraft_Console_App.Interface_Elements;
using TimeCraft_Console_App.Interface_Elements.Forms;
using TimeCraft_Console_App.Models;

namespace TimeCraft_Console_App.Views
{
    internal class EditPlanView
    {
        public IPlan? displayEditPlanForm(IPlan? plan)
        {
            Console.Clear();
            EditPlanForm editPlanForm = new EditPlanForm();
            IPlan? editedPlan = editPlanForm.render(plan);
            Console.Clear();
            return editedPlan;
        }
    }
}
