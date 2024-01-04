using SharedLibrary.Models;
using SharedLibrary.Repositories;
using SharedLibrary.Services;
using System.Windows.Input;
using TimeCraft_WPF_App.Core;

namespace TimeCraft_WPF_App.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase? _currentTab;

        public ViewModelBase? CurrentTab
        {
            get { return _currentTab; }
            set
            {
                if (_currentTab != value)
                {
                    _currentTab = value;
                    OnPropertyChanged(nameof(CurrentTab));
                }
            }
        }

        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCalendarViewCommand { get; }
        public ICommand AddMeetingViewCommand { get; }
        public ICommand AddTaskViewCommand { get; }
        public ICommand LogoutCommand { get; }

        public ICommand SignInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        private bool isUserLoggedIn;

        private int selectedTabIndex;

        private bool shouldValidateLoginForm;
        private bool shouldValidateRegistrationForm;

        private UsersService usersService;
        private UserSession userSession;

        public MainWindowViewModel()
        {
            shouldValidateLoginForm = false;
            shouldValidateRegistrationForm = false;

            usersService = UsersService.Instance;
            userSession = UserSession.Instance;

            CurrentTab = new LoginViewModel();
            ShowHomeViewCommand = new RelayCommand(ExecuteShowHomeView, CanExecuteCommand);
            ShowCalendarViewCommand = new RelayCommand(ExecuteShowCalendarView, CanExecuteCommand);
            AddMeetingViewCommand = new RelayCommand(ExecuteAddMeetingView, CanExecuteCommand);
            AddTaskViewCommand = new RelayCommand(ExecuteAddTaskView, CanExecuteCommand);
            LogoutCommand = new RelayCommand(ExecuteLogout, CanExecuteCommand);

            SignInCommand = new RelayCommand(_ => SignIn(), _ => CanSignIn());
            SignUpCommand = new RelayCommand(_ => SignUp(), _ => CanSignUp());
        }

        private bool isLogoutBtnChecked;

        public bool IsLogoutBtnChecked
        {
            get { return isLogoutBtnChecked; }
            set
            {
                if (isLogoutBtnChecked != value)
                {
                    isLogoutBtnChecked = value;
                    OnPropertyChanged(nameof(IsLogoutBtnChecked));
                }
            }
        }

        private bool isSignInBtnChecked;
        public bool IsHomeBtnChecked
        {
            get { return isSignInBtnChecked; }
            set
            {
                if (isSignInBtnChecked != value)
                {
                    isSignInBtnChecked = value;
                    OnPropertyChanged(nameof(IsHomeBtnChecked));
                }
            }
        }

        public int SelectedTabIndex
        {
            get { return selectedTabIndex; }
            set
            {
                if (selectedTabIndex != value)
                {
                    selectedTabIndex = value;
                    OnPropertyChanged(nameof(SelectedTabIndex));
                    TabChange();
                    ResetLoginForm();
                    ResetRegistrationForm();
                    shouldValidateLoginForm = false;
                    shouldValidateRegistrationForm = false;
                    LoginErrorMessage = null;
                    RegistrationSuccessfulMessage = null;
                }
            }
        }

        public bool IsUserLoggedIn
        {
            get { return isUserLoggedIn; }
            set
            {
                if (isUserLoggedIn != value)
                {
                    isUserLoggedIn = value;
                    OnPropertyChanged(nameof(IsUserLoggedIn));
                    ResetLoginForm();
                    ResetRegistrationForm();
                    shouldValidateLoginForm = false;
                    shouldValidateRegistrationForm = false;
                    LoginErrorMessage = null;
                    RegistrationSuccessfulMessage = null;
                }
            }
        }

        private void ExecuteShowHomeView(object? parameter)
        {
            CurrentTab = new HomeTabViewModel();
        }

        private void ExecuteShowCalendarView(object? parameter)
        {
            CurrentTab = new PlansViewModel();
        }

        private void ExecuteAddMeetingView(object? parameter)
        {
            CurrentTab = new AddMeetingViewModel();
        }

        private void ExecuteAddTaskView(object? parameter)
        {
            CurrentTab = new AddTaskViewModel();
        }

        private void ExecuteLogout(object? parameter)
        {
            IsUserLoggedIn = false;
            IsLogoutBtnChecked = false;
            CurrentTab = new LoginViewModel();
        }

        private bool CanExecuteCommand(object? parameter)
        {
            return IsUserLoggedIn;
        }

        private void TabChange()
        {
            ClearFormErrors();
            if (SelectedTabIndex == 0)
            {
                CurrentTab = new LoginViewModel();
            }
            else if (SelectedTabIndex == 1)
            {
                CurrentTab = new RegistrationViewModel();
            }
        }

        private void ClearFormErrors()
        {
            ClearErrors(nameof(EmailLogin));
            ClearErrors(nameof(PasswordLogin));
            ClearErrors(nameof(FirstNameRegistration));
            ClearErrors(nameof(LastNameRegistration));
            ClearErrors(nameof(EmailRegistration));
            ClearErrors(nameof(PasswordRegistration));
        }

        private string? firstNameRegistration;
        private string? lastNameRegistration;
        private string? emailRegistration;
        private string? passwordRegistration;

        public string? FirstNameRegistration
        {
            get { return firstNameRegistration; }
            set
            {
                if (firstNameRegistration != value)
                {
                    firstNameRegistration = value;
                    OnPropertyChanged(nameof(FirstNameRegistration));
                }
            }
        }

        public string? LastNameRegistration
        {
            get { return lastNameRegistration; }
            set
            {
                if (lastNameRegistration != value)
                {
                    lastNameRegistration = value;
                    OnPropertyChanged(nameof(LastNameRegistration));
                }
            }
        }

        public string? EmailRegistration
        {
            get { return emailRegistration; }
            set
            {
                if (emailRegistration != value)
                {
                    emailRegistration = value;
                    OnPropertyChanged(nameof(EmailRegistration));
                }
            }
        }

        public string? PasswordRegistration
        {
            get { return passwordRegistration; }
            set
            {
                if (passwordRegistration != value)
                {
                    passwordRegistration = value;
                    OnPropertyChanged(nameof(PasswordRegistration));
                }
            }
        }

        private string? emailLogin;
        private string? passwordLogin;

        public string? EmailLogin
        {
            get { return emailLogin; }
            set
            {
                if (emailLogin != value)
                {
                    emailLogin = value;
                    OnPropertyChanged(nameof(EmailLogin));
                }
            }
        }

        public string? PasswordLogin
        {
            get { return passwordLogin; }
            set
            {
                if (passwordLogin != value)
                {
                    passwordLogin = value;
                    OnPropertyChanged(nameof(PasswordLogin));
                }
            }
        }

        private void ResetLoginForm()
        {
            EmailLogin = null;
            PasswordLogin = null;
        }

        private void ResetRegistrationForm()
        {
            FirstNameRegistration = null;
            LastNameRegistration = null;
            EmailRegistration = null;
            PasswordRegistration = null;
        }

        private void ValidateLoginForm()
        {
            ClearErrors(nameof(EmailLogin));
            ClearErrors(nameof(PasswordLogin));

            if (shouldValidateLoginForm)
            {
                if (string.IsNullOrEmpty(EmailLogin))
                {
                    AddError(nameof(EmailLogin), "Email is required.");
                }

                if (string.IsNullOrEmpty(PasswordLogin))
                {
                    AddError(nameof(PasswordLogin), "Password is required.");
                }
            }
        }

        private bool CanSignIn()
        {
            if (CurrentTab is LoginViewModel)
            {
                ValidateLoginForm();
                return !HasErrors;
            }
            return false;
        }

        private void SignIn()
        {
            shouldValidateLoginForm = true;
            LoginErrorMessage = null;

            if (CanSignIn())
            {
                if (usersService.UsersRepository.userExists(EmailLogin!, PasswordLogin!))
                {
                    User? loggedUser = usersService.UsersRepository.getUser(EmailLogin!, PasswordLogin!);
                    userSession.LoggedUser = loggedUser;

                    IsUserLoggedIn = true;
                    IsHomeBtnChecked = true;
                    CurrentTab = new HomeTabViewModel();
                }
                else
                {
                    shouldValidateLoginForm = false;
                    LoginErrorMessage = "Invalid email or password. Please try again.";
                    ResetLoginForm();
                }
            }
        }

        private void ValidateRegistrationForm()
        {
            ClearErrors(nameof(FirstNameRegistration));
            ClearErrors(nameof(LastNameRegistration));
            ClearErrors(nameof(EmailRegistration));
            ClearErrors(nameof(PasswordRegistration));

            if (shouldValidateRegistrationForm)
            {
                if (string.IsNullOrEmpty(FirstNameRegistration))
                {
                    AddError(nameof(FirstNameRegistration), "First name is required.");
                }

                if (string.IsNullOrEmpty(LastNameRegistration))
                {
                    AddError(nameof(LastNameRegistration), "Last name is required.");
                }

                if (string.IsNullOrEmpty(EmailRegistration))
                {
                    AddError(nameof(EmailRegistration), "Email is required.");
                }
                else if (!IsValidEmail(EmailRegistration))
                {
                    AddError(nameof(EmailRegistration), "Invalid email format.");
                }
                else if (usersService.UsersRepository.emailExists(EmailRegistration))
                {
                    AddError(nameof(EmailRegistration), "Email already exists.");
                }

                if (string.IsNullOrEmpty(PasswordRegistration))
                {
                    AddError(nameof(PasswordRegistration), "Password is required.");
                }
            }

        }

        private bool IsValidEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email,
                @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
        }

        private bool CanSignUp()
        {
            if (CurrentTab is RegistrationViewModel)
            {
                ValidateRegistrationForm();
                return !HasErrors;
            }
            return false;
        }

        private void SignUp()
        {
            shouldValidateRegistrationForm = true;
            RegistrationSuccessfulMessage = null;

            if (CanSignUp())
            {
                var newUser = new User(FirstNameRegistration!, LastNameRegistration!, EmailRegistration!, PasswordRegistration!);

                usersService.UsersRepository.addUser(newUser);

                shouldValidateRegistrationForm = false;
                ResetRegistrationForm();
                RegistrationSuccessfulMessage = "Registration completed successfully. You can now log in to your account.";
            }
        }

        private string? loginErrorMessage;

        public string? LoginErrorMessage
        {
            get { return loginErrorMessage; }
            set
            {
                if (loginErrorMessage != value)
                {
                    loginErrorMessage = value;
                    OnPropertyChanged(nameof(LoginErrorMessage));
                }
            }
        }

        private string? registrationSuccessfulMessage;

        public string? RegistrationSuccessfulMessage
        {
            get { return registrationSuccessfulMessage; }
            set
            {
                if (registrationSuccessfulMessage != value)
                {
                    registrationSuccessfulMessage = value;
                    OnPropertyChanged(nameof(RegistrationSuccessfulMessage));
                }
            }
        }
    }
}
