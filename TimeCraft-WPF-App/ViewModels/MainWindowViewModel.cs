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
        public ICommand ExitCommand { get; }

        public ICommand ShowLoginFormCommand { get; }
        public ICommand ShowRegistrationFormCommand { get; }

        private bool isUserLoggedIn;

        public MainWindowViewModel()
        {
            CurrentTab = new HomeTabViewModel();
            ShowHomeViewCommand = new RelayCommand(ExecuteShowHomeView, CanExecuteCommand);
            ShowCalendarViewCommand = new RelayCommand(ExecuteShowCalendarView, CanExecuteCommand);
            AddMeetingViewCommand = new RelayCommand(ExecuteAddMeetingView, CanExecuteCommand);
            AddTaskViewCommand = new RelayCommand(ExecuteAddTaskView, CanExecuteCommand);
            ExitCommand = new RelayCommand(ExecuteExit, CanExecuteCommand);

            ShowLoginFormCommand = new RelayCommand(ExecuteShowLoginForm, CanExecuteShowLoginForm);
            ShowRegistrationFormCommand = new RelayCommand(ExecuteShowRegistrationForm, CanExecuteShowRegistrationForm);
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
                }
            }
        }

        private void ExecuteShowLoginForm(object? parameter)
        {
            CurrentTab = new LoginViewModel();
        }

        private bool CanExecuteShowLoginForm(object? parameter)
        {
            return !IsUserLoggedIn;
        }

        private void ExecuteShowRegistrationForm(object? parameter)
        {
            CurrentTab = new RegistrationViewModel();
        }

        private bool CanExecuteShowRegistrationForm(object? parameter)
        {
            return !IsUserLoggedIn;
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

        private void ExecuteExit(object? parameter)
        {
            Environment.Exit(0);
        }

        private bool CanExecuteCommand(object? parameter)
        {
            return true;
        }
    }
}
