using SharedLibrary.Models;
using SharedLibrary.Repositories;
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
        private UserSession userSession;

        private ICollectionView? meetingsView;
        private ICollectionView? tasksView;

        private IPlan? activityToDelete;

        private string? selectedSortType;
        private string? selectedPriority;

        private bool isDialogOpen;
        public bool IsDialogOpen
        {
            get { return isDialogOpen; }
            set
            {
                if (isDialogOpen != value)
                {
                    isDialogOpen = value;
                    OnPropertyChanged(nameof(IsDialogOpen));
                }
            }
        }

        public ICommand SaveActivityToDeleteCommand { get; set; }
        public ICommand DeleteActivityCommand { get; set; }

        public PlansViewModel()
        {
            plansService = PlansService.Instance;
            userSession = UserSession.Instance;

            IsDialogOpen = false;

            SaveActivityToDeleteCommand = new RelayCommand(SaveActivityToDeleteCommandExecute, CanSaveActivityToDeleteCommandExecute);
            DeleteActivityCommand = new RelayCommand(DeleteActivityCommandExecute, CanDeleteActivityCommandExecute);

            this.kindsOfMeetings = new ObservableCollection<string>(plansService.PlansRepository.KindsOfMeetings);
            this.priorities = new ObservableCollection<string>(plansService.PlansRepository.Priorities);

            SelectedDate = DateTime.Now;
            LoadPlans();
        }

        private void DeleteActivityCommandExecute(object? parameter)
        {
            if (activityToDelete is Meeting meeting)
            {
                Meetings?.Remove(meeting);
                plansService.PlansRepository.deleteMeeting(meeting.MeetingId);
            }

            if (activityToDelete is SharedLibrary.Models.Task task)
            {
                Tasks?.Remove(task);
                plansService.PlansRepository.deleteTask(task.TaskId);
            }

            IsDialogOpen = false;
        }

        private bool CanDeleteActivityCommandExecute(object? parameter)
        {
            return activityToDelete != null;
        }

        private void SaveActivityToDeleteCommandExecute(object? parameter)
        {
            if (parameter is IPlan plan)
            {
                IsDialogOpen = true;
                activityToDelete = plan;
            }
        }

        private bool CanSaveActivityToDeleteCommandExecute(object? parameter)
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
            Meetings = new ObservableCollection<Meeting>(PlansService.Instance.PlansRepository.getMeetingsById(userSession.LoggedUser!.Id, SelectedDate));
            Tasks = new ObservableCollection<SharedLibrary.Models.Task>(PlansService.Instance.PlansRepository.getTasksById(userSession.LoggedUser!.Id, SelectedDate));

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
