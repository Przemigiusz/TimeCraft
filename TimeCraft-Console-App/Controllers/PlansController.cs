using SharedLibrary.Models;
using SharedLibrary.Repositories;
using SharedLibrary.Services;
using TimeCraft_Console_App.Messages;
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

        private LoginOrRegistrationView loginOrRegistrationView;
        private LoginFormView loginFormView;
        private RegistrationFormView registrationFormView;

        private CalendarService calendarService;
        private PlansService plansService;
        private UsersService usersService;

        private UserSession userSession;

        public PlansController()
        {
            this.startingMenuView = new StartingMenuView();
            this.calendarView = new CalendarView();
            this.addPlansView = new AddPlansView();
            this.plansView = new PlansView();
            this.editPlanView = new EditPlanView();
            this.planDetailsView = new PlanDetailsView();

            this.loginOrRegistrationView = new LoginOrRegistrationView();
            this.loginFormView = new LoginFormView();
            this.registrationFormView = new RegistrationFormView();

            this.calendarService = CalendarService.Instance;
            this.plansService = PlansService.Instance;
            this.usersService = UsersService.Instance;
            this.userSession = UserSession.Instance;
        }

        private List<string> validateRegistrationForm(RegisteredUser user)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(user.FirstName))
            {
                errors.Add("First name is required.");
            }
            if (string.IsNullOrEmpty(user.LastName))
            {
                errors.Add("Last name is required.");
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                errors.Add("Email is required.");
            }
            else if (!isValidEmail(user.Email))
            {
                errors.Add("Invalid email format.");
            }
            else if (usersService.UsersRepository.emailExists(user.Email))
            {
                errors.Add("Email already exists.");
            }


            if (string.IsNullOrEmpty(user.Password))
            {
                errors.Add("Password is required.");
            }
            else if (user.Password != user.RepeatedPassword)
            {
                errors.Add("Passwords are not the same.");
            }

            return errors;
        }

        private bool isValidEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email,
                @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
        }

        private List<string> validateLoginForm(Login login)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(login.Email))
            {
                errors.Add("Email is required.");
            }
            if (string.IsNullOrEmpty(login.Password))
            {
                errors.Add("Password is required.");
            }
            return errors;
        }

        public void takeControl()
        {
            List<string> errors;
            int result = this.startingMenuView.DisplayStartingMenu().Result;
            switch (result)
            {
                case Codes.SMContinue:
                    while (true)
                    {
                        result = this.loginOrRegistrationView.displayMenu();
                        switch (result)
                        {
                            case Codes.LoRLogin:
                                Login login = loginFormView.displayLoginForm();
                                errors = validateLoginForm(login);
                                if (errors.Count == 0)
                                {
                                    if (this.usersService.UsersRepository.userExists(login.Email, login.Password))
                                    {
                                        this.userSession.LoggedUser = this.usersService.UsersRepository.getUser(login.Email, login.Password);
                                        this.displayCalendar();
                                        this.userSession.LoggedUser = null;
                                    }
                                    else
                                    {
                                        errors.Add("Invalid email or password. Please try again.");
                                        this.loginFormView.displayValidationErrors(errors);
                                    }
                                }
                                else
                                {
                                    this.loginFormView.displayValidationErrors(errors);
                                }
                                break;
                            case Codes.LoRRegistration:
                                RegisteredUser user = registrationFormView.displayRegistrationForm();
                                errors = validateRegistrationForm(user);
                                if (errors.Count == 0)
                                {
                                    User newUser = new User(user.FirstName, user.LastName, user.Email, user.Password);
                                    this.usersService.UsersRepository.addUser(newUser);
                                    this.registrationFormView.displaySuccessMessage();
                                }
                                else
                                {
                                    this.loginFormView.displayValidationErrors(errors);
                                }
                                break;
                            default:
                                System.Environment.Exit(0);
                                break;
                        }
                    }
                case Codes.SMExit:
                    System.Environment.Exit(0);
                    break;
            }
        }
        public void displayCalendar()
        {
            CNavigationResult result;
            while (true)
            {
                result = this.calendarView.displayCalendar(calendarService.getMonths(), calendarService.getDayAbbreviations(), plansService.PlansRepository.Meetings, plansService.PlansRepository.Tasks);
                switch (result.Code)
                {
                    case Codes.WTDMenuCTDAddPlans:
                        this.displayAddPlansForm(result.ChosenDate);
                        break;
                    case Codes.WTDMenuCTDShowPlans:
                        this.displayPlansForDate(result.ChosenDate);
                        break;
                    case Codes.CLogout:
                        return;
                }
            }
        }
        private void displayAddPlansForm(DateTime chosenDate)
        {
            IPlan result = this.addPlansView.displayNewPlanForm(chosenDate);
            if (result is Meeting)
            {
                Meeting newMeeting = (Meeting)result;
                newMeeting.UserId = this.userSession.LoggedUser!.Id;
                this.plansService.PlansRepository.addMeeting((Meeting)result);
            }
            else if (result is SharedLibrary.Models.Task)
            {
                SharedLibrary.Models.Task newTask = (SharedLibrary.Models.Task)result;
                newTask.UserId = this.userSession.LoggedUser!.Id;
                this.plansService.PlansRepository.addTask((SharedLibrary.Models.Task)result);
            }
        }
        private void displayPlansForDate(DateTime chosendate)
        {
            DSNavigationResult result;
            List<Meeting> meetings = this.plansService.PlansRepository.getMeetingsById(this.userSession.LoggedUser!.Id, chosendate);
            List<SharedLibrary.Models.Task> tasks = this.plansService.PlansRepository.getTasksById(this.userSession.LoggedUser!.Id, chosendate);
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
                            }
                            else if (result.TypeIndicator == typeof(SharedLibrary.Models.Task).Name)
                            {
                                this.plansService.PlansRepository.updateTask((SharedLibrary.Models.Task)plan);
                            }
                        }
                        break;
                    case Codes.DSDelete:
                        if (result.TypeIndicator == typeof(Meeting).Name)
                        {
                            this.plansService.PlansRepository.deleteMeeting(result.Content);
                            meetings = this.plansService.PlansRepository.getMeetingsById(this.userSession.LoggedUser!.Id, chosendate);
                        }
                        else
                        {
                            this.plansService.PlansRepository.deleteTask(result.Content);
                            tasks = this.plansService.PlansRepository.getTasksById(this.userSession.LoggedUser!.Id, chosendate);
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
