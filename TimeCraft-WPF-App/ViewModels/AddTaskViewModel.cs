using SharedLibrary.Repositories;
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
        private UserSession userSession;

        public ICommand AddTaskCommand { get; set; }

        private bool shouldValidateAddTaskForm;

        public AddTaskViewModel()
        {
            shouldValidateAddTaskForm = false;

            plansService = PlansService.Instance;
            userSession = UserSession.Instance;
            priorities = new ObservableCollection<string>(plansService.PlansRepository.Priorities);
            AddTaskCommand = new RelayCommand(_ => AddTask(), _ => CanAddTask());
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

        private bool CanAddTask()
        {
            ValidateAddTaskForm();
            return !HasErrors;
        }

        private void AddTask()
        {
            shouldValidateAddTaskForm = true;
            AddedSuccessfullyMessage = null;

            if (CanAddTask())
            {
                string formattedDate = SelectedDate!.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                SharedLibrary.Models.Task newTask = new SharedLibrary.Models.Task(Name!, Description!, formattedDate, SelectedPriority!, false, userSession.LoggedUser!.Id);
                plansService.PlansRepository.addTask(newTask);

                shouldValidateAddTaskForm = false;
                AddedSuccessfullyMessage = "The task was added successfully.";

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

        private void ClearFormErrors()
        {
            ClearErrors(nameof(SelectedDate));
            ClearErrors(nameof(Name));
            ClearErrors(nameof(SelectedPriority));
            ClearErrors(nameof(Description));
        }

        private void ValidateAddTaskForm()
        {
            ClearFormErrors();

            if (shouldValidateAddTaskForm)
            {
                if (!SelectedDate.HasValue)
                {
                    AddError(nameof(SelectedDate), "Date is required.");
                }

                if (string.IsNullOrEmpty(Name))
                {
                    AddError(nameof(Name), "Task name is required.");
                }

                if (string.IsNullOrEmpty(SelectedPriority))
                {
                    AddError(nameof(SelectedPriority), "Task priority is required.");
                }

                if (string.IsNullOrEmpty(Description))
                {
                    AddError(nameof(Description), "Task description is required.");
                }
            }
        }

        private string? addedSuccessfullyMessage;

        public string? AddedSuccessfullyMessage
        {
            get { return addedSuccessfullyMessage; }
            set
            {
                if (addedSuccessfullyMessage != value)
                {
                    addedSuccessfullyMessage = value;
                    OnPropertyChanged(nameof(AddedSuccessfullyMessage));
                }
            }
        }
    }
}
