using TimeCraft_Console_App.Messages;
using TimeCraft_Console_App.Models;
using TimeCraft_Console_App.Services;
using TimeCraft_Console_App.Views;

namespace TimeCraft_Console_App.Controllers
{
    internal class PlansController
    {
        private StartingMenuView startingMenuView;
        private CalendarView calendarView;
        private AddPlansView addPlansView;
        private PlansView plansView;
        private EditPlanView editPlanView;
        private PlanDetailsView planDetailsView;

        private CalendarService calendarService;
        private PlansService plansService;

        public PlansController()
        {
            this.startingMenuView = new StartingMenuView();
            this.calendarView = new CalendarView();
            this.addPlansView = new AddPlansView();
            this.plansView = new PlansView();
            this.editPlanView = new EditPlanView();
            this.planDetailsView = new PlanDetailsView();

            this.calendarService = CalendarService.Instance;
            this.plansService = PlansService.Instance;
        }

        public void takeControl() {
            int result = this.startingMenuView.DisplayStartingMenu().Result;
            switch (result)
            {
                case Codes.SMContinue:
                    this.displayCalendar();
                    break;
                case Codes.SMExit:
                    Console.Clear();
                    System.Environment.Exit(0);
                    break;
            }
        }
        public void displayCalendar() {
            CNavigationResult result;
            while (true) {
                result = this.calendarView.displayCalendar(calendarService.getMonths(), calendarService.getDayAbbreviations(), plansService.PlansRepository.Meetings, plansService.PlansRepository.Tasks);
                switch (result.Code)
                {
                    case Codes.WTDMenuCTDAddPlans:
                        this.displayAddPlansForm(result.ChosenDate);
                        break;
                    case Codes.WTDMenuCTDShowPlans:
                        this.displayPlansForDate(result.ChosenDate);
                        break;
                    case Codes.Exit:
                        Console.Clear();
                        System.Environment.Exit(0);
                        break;
                }
            }
        }
        private void displayAddPlansForm(DateTime chosenDate)
        {
            IPlan result = this.addPlansView.displayNewPlanForm(chosenDate);
            if (result is Meeting)
            {
                this.plansService.PlansRepository.addMeeting((Meeting)result);
            }
            else if (result is Models.Task) {
                this.plansService.PlansRepository.addTask((Models.Task)result);
            }
        }
        private void displayPlansForDate(DateTime chosendate)
        {
            DSNavigationResult result;
            List <Meeting> meetings = this.plansService.PlansRepository.getMeetings(chosendate);
            List<Models.Task> tasks = this.plansService.PlansRepository.getTasks(chosendate);
            while (true)
            {
                result = this.plansView.displayPlans(meetings, tasks);
                int resultCode = result.Code;
                switch (resultCode)
                {
                    case Codes.DSDisplayDetails:
                        this.displayPlanDetails(result.Content, result.TypeIndicator);
                        break;
                    case Codes.DSEdit:
                        IPlan? plan = this.displayEditPlanForm(result.Content, result.TypeIndicator);
                        if (plan is not null)
                        {
                            if (result.TypeIndicator == typeof(Meeting).Name)
                            {
                                this.plansService.PlansRepository.updateMeeting((Meeting)plan);
                                meetings = this.plansService.PlansRepository.getMeetings(chosendate);
                            }
                            else if (result.TypeIndicator == typeof(Models.Task).Name)
                            {
                                this.plansService.PlansRepository.updateTask((Models.Task)plan);
                                tasks = this.plansService.PlansRepository.getTasks(chosendate);
                            }
                        }
                        break;
                    case Codes.DSDelete:
                        if (result.TypeIndicator == typeof(Meeting).Name)
                        {
                            this.plansService.PlansRepository.deleteMeeting(result.Content);
                            meetings = this.plansService.PlansRepository.getMeetings(chosendate);
                        }
                        else
                        {
                            this.plansService.PlansRepository.deleteTask(result.Content);
                            tasks = this.plansService.PlansRepository.getTasks(chosendate);
                        }
                        break;
                    case Codes.DSxit:
                        return;
                }
            }
        }
        private void displayPlanDetails(int planId, string typeIndicator)
        {
            IPlan? plan;
            if (typeIndicator == typeof(Meeting).Name)
            {
                plan = this.plansService.PlansRepository.getMeeting(planId);
            }
            else
            {
                plan = this.plansService.PlansRepository.getTask(planId);
            }
            this.planDetailsView.displayPlanDetails(plan);
        }
        private IPlan? displayEditPlanForm(int planId, string typeIndicator)
        {
            IPlan? plan;
            if (typeIndicator == typeof(Meeting).Name)
            {
                plan = this.plansService.PlansRepository.getMeeting(planId);
            }
            else
            {
                plan = this.plansService.PlansRepository.getTask(planId);
            }
            return this.editPlanView.displayEditPlanForm(plan);
        }
    }
}
