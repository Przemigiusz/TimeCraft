using SharedLibrary.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using TimeCraft_WPF_App.Core;

namespace TimeCraft_WPF_App.ViewModels
{
    class AddTaskViewModel : ViewModelBase
    {
        private DateTime? selectedDate;
        private string? name;
        private string? selectedPriority;
        private string? description;

        private ObservableCollection<string> priorities;

        private PlansService plansService;

        public ICommand AddTaskCommand { get; set; }

        public AddTaskViewModel()
        {
            plansService = PlansService.Instance;
            priorities = new ObservableCollection<string>(plansService.PlansRepository.Priorities);
            AddTaskCommand = new RelayCommand(_ => AddTask(), _ => CanAddTask());
        }

        private bool CanAddTask()
        {
            return SelectedDate.HasValue
                && !string.IsNullOrEmpty(Name)
                && !string.IsNullOrEmpty(SelectedPriority)
                && !string.IsNullOrEmpty(Description);
        }

        public DateTime? SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }

        public string? Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
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
                    selectedPriority = value;
                    OnPropertyChanged(nameof(SelectedPriority));
                }
            }
        }

        public string? Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public ObservableCollection<string> Priorities
        {
            get { return priorities; }
        }

        private void AddTask()
        {
            if (SelectedDate.HasValue && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(SelectedPriority) && !string.IsNullOrEmpty(Description))
            {
                string formattedDate = SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                SharedLibrary.Models.Task newTask = new SharedLibrary.Models.Task(Name, Description, formattedDate, SelectedPriority, false);
                plansService.PlansRepository.addTask(newTask);

                ResetForm();
            }
        }

        private void ResetForm()
        {
            SelectedDate = null;
            Name = null;
            SelectedPriority = null;
            Description = null;
        }
    }
}
