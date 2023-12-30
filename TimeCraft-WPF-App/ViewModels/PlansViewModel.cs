using SharedLibrary.Models;
using SharedLibrary.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using TimeCraft_WPF_App.Core;

namespace TimeCraft_WPF_App.ViewModels
{
    class PlansViewModel : ViewModelBase
    {
        private DateTime selectedDate;
        private ObservableCollection<Meeting>? meetings;
        private ObservableCollection<SharedLibrary.Models.Task>? tasks;

        private ObservableCollection<string> kindsOfMeetings;
        private ObservableCollection<string> priorities;

        private PlansService plansService;

        private ICollectionView? meetingsView;
        private ICollectionView? tasksView;

        private string? selectedSortType;
        private string? selectedPriority;

        public ICommand DeleteMeetingCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }

        public PlansViewModel()
        {
            plansService = PlansService.Instance;
            DeleteMeetingCommand = new RelayCommand(DeleteMeetingCommandExecute, CanDeleteCommandExecute);
            DeleteTaskCommand = new RelayCommand(DeleteTaskCommandExecute, CanDeleteCommandExecute);

            

            this.kindsOfMeetings = new ObservableCollection<string>(plansService.PlansRepository.KindsOfMeetings);
            this.priorities = new ObservableCollection<string>(plansService.PlansRepository.Priorities);

            SelectedDate = DateTime.Now;
            LoadPlans();
        }

        private void DeleteMeetingCommandExecute(object? parameter)
        {
            if (parameter is Meeting meeting)
            {
                Meetings?.Remove(meeting);
            }
        }

        private void DeleteTaskCommandExecute(object? parameter)
        {
            if (parameter is SharedLibrary.Models.Task task)
            {
                Tasks?.Remove(task);
            }
        }

        private bool CanDeleteCommandExecute(object? parameter)
        {
            return parameter != null;
        }

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    LoadPlans();
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }

        public ObservableCollection<Meeting>? Meetings
        {
            get { return meetings; }
            set
            {
                if (meetings != value)
                {
                    meetings = value;
                    OnPropertyChanged(nameof(Meetings));
                }
            }
        }

        public ObservableCollection<SharedLibrary.Models.Task>? Tasks
        {
            get { return tasks; }
            set
            {
                if (tasks != value)
                {
                    tasks = value;
                    OnPropertyChanged(nameof(Tasks));
                }
            }
        }

        public ObservableCollection<string> KindsOfMeetings
        {
            get { return kindsOfMeetings; }
        }

        public ObservableCollection<string> Priorities
        {
            get { return priorities; }
        }

        public string? SelectedSortType
        {
            get { return selectedSortType; }
            set
            {
                if (selectedSortType != value)
                {
                    selectedSortType = value?.ToString()?.Split(':').LastOrDefault()?.Trim();
                    ApplySort();
                    OnPropertyChanged(nameof(SelectedSortType));
                }
            }
        }

        public string? SelectedPriority
        {
            get { return selectedPriority; }
            set
            {
                if (selectedPriority != value)
                {
                    selectedPriority = value?.ToString()?.Split(':').LastOrDefault()?.Trim();
                    ApplyFilter();
                    OnPropertyChanged(nameof(SelectedPriority));
                }
            }
        }

        private void LoadPlans()
        {
            Meetings = new ObservableCollection<Meeting>(PlansService.Instance.PlansRepository.getMeetings(SelectedDate));
            Tasks = new ObservableCollection<SharedLibrary.Models.Task>(PlansService.Instance.PlansRepository.getTasks(SelectedDate));

            meetingsView = CollectionViewSource.GetDefaultView(Meetings);
            tasksView = CollectionViewSource.GetDefaultView(Tasks);
        }

        private void ApplySort()
        {
            if (meetingsView != null)
            {
                meetingsView.SortDescriptions.Clear();

                if (SelectedSortType == "Ascending")
                {
                    meetingsView.SortDescriptions.Add(new SortDescription("MeetingStartTimeDate", ListSortDirection.Ascending));
                }
                else if (SelectedSortType == "Descending")
                {
                    meetingsView.SortDescriptions.Add(new SortDescription("MeetingStartTimeDate", ListSortDirection.Descending));
                }
            }
        }

        private void ApplyFilter()
        {
            if (tasksView != null)
            {
                tasksView.Filter = task =>
                {
                    if (task is SharedLibrary.Models.Task t)
                    {
                        string selectedPriorityValue = selectedPriority?.ToString() ?? "";

                        if (selectedPriorityValue == "All")
                        {
                            return true;
                        }

                        return string.IsNullOrEmpty(selectedPriorityValue) || t.TaskPriority == selectedPriorityValue;
                    }

                    return false;
                };
            }
        }
    }
}
