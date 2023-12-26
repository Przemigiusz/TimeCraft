using System.ComponentModel;
using System.Windows.Input;
using TimeCraft_WPF_App.Core;

namespace TimeCraft_WPF_App.ViewModels
{
    public class CalendarViewModel : ViewModelBase
    {
        private readonly INavigation _navigation;

        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                    UpdateButtonsVisibility();
                }
            }
        }

        private bool _isButtonsVisible;

        public bool IsButtonsVisible
        {
            get { return _isButtonsVisible; }
            set
            {
                if (_isButtonsVisible != value)
                {
                    _isButtonsVisible = value;
                    OnPropertyChanged(nameof(IsButtonsVisible));
                }
            }
        }

        public ICommand AddMeetingCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand ShowPlansCommand { get; }

        public CalendarViewModel(INavigation navigation)
        {
            AddMeetingCommand = new RelayCommand(ExecuteAddMeeting, CanExecuteCommand);
            AddTaskCommand = new RelayCommand(ExecuteAddTask, CanExecuteCommand);
            ShowPlansCommand = new RelayCommand(ExecuteShowPlans, CanExecuteCommand);
            IsButtonsVisible = false;
            PropertyChanged += CalendarViewModel_PropertyChanged;
            _navigation = navigation;
        }

        private void CalendarViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedDate))
            {
                UpdateButtonsVisibility();
            }
        }

        private void UpdateButtonsVisibility()
        {
            IsButtonsVisible = SelectedDate != default(DateTime);
        }

        private void ExecuteAddMeeting(object? parameter)
        {
            _navigation.NavigateTo(new AddMeetingViewModel());
        }

        private void ExecuteAddTask(object? parameter)
        {
            _navigation.NavigateTo(new AddTaskViewModel());
        }

        private void ExecuteShowPlans(object? parameter)
        {
            _navigation.NavigateTo(new ShowPlansViewModel());
        }

        private bool CanExecuteCommand(object? parameter)
        {
            return true;
        }
    }
}
