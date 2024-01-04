using SharedLibrary.Models;
using SharedLibrary.Repositories;
using SharedLibrary.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using TimeCraft_WPF_App.Core;

namespace TimeCraft_WPF_App.ViewModels
{
    class AddMeetingViewModel : ViewModelBase
    {
        private DateTime? selectedDate;
        private string? selectedType;
        private string? topic;
        private DateTime? startTime;
        private DateTime? endTime;

        private ObservableCollection<string> kindsOfMeetings;

        private PlansService plansService;
        private UserSession userSession;

        public ICommand AddMeetingCommand { get; set; }

        private bool shouldValidateAddMeetingForm;

        public AddMeetingViewModel()
        {
            shouldValidateAddMeetingForm = false;

            plansService = PlansService.Instance;
            userSession = UserSession.Instance;
            kindsOfMeetings = new ObservableCollection<string>(plansService.PlansRepository.KindsOfMeetings);
            AddMeetingCommand = new RelayCommand(_ => AddMeeting(), _ => CanAddMeeting());
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

        public string? SelectedType
        {
            get { return selectedType; }
            set
            {
                if (selectedType != value)
                {
                    selectedType = value;
                    OnPropertyChanged(nameof(SelectedType));
                }
            }
        }

        public string? Topic
        {
            get { return topic; }
            set
            {
                if (topic != value)
                {
                    topic = value;
                    OnPropertyChanged(nameof(Topic));
                }
            }
        }

        public DateTime? StartTime
        {
            get { return startTime; }
            set
            {
                if (startTime != value)
                {
                    startTime = value;
                    OnPropertyChanged(nameof(StartTime));
                }
            }
        }

        public DateTime? EndTime
        {
            get { return endTime; }
            set
            {
                if (endTime != value)
                {
                    endTime = value;
                    OnPropertyChanged(nameof(EndTime));
                }
            }
        }

        public ObservableCollection<string> KindsOfMeetings
        {
            get { return kindsOfMeetings; }
        }

        private bool CanAddMeeting()
        {
            ValidateAddMeetingForm();
            return !HasErrors;
        }

        private void AddMeeting()
        {
            shouldValidateAddMeetingForm = true;
            AddedSuccessfullyMessage = null;

            if (CanAddMeeting())
            {
                string formattedDate = SelectedDate!.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                string formattedStartTime = StartTime!.Value.ToString("HH:mm", CultureInfo.InvariantCulture);
                string formattedEndTime = EndTime!.Value.ToString("HH:mm", CultureInfo.InvariantCulture);

                Meeting newMeeting = new Meeting(SelectedType!, Topic!, formattedDate, formattedStartTime, formattedEndTime, userSession.LoggedUser!.Id);
                plansService.PlansRepository.addMeeting(newMeeting);

                shouldValidateAddMeetingForm = false;
                AddedSuccessfullyMessage = "The meeting was added successfully.";

                ResetForm();
            }
        }

        private void ResetForm()
        {
            SelectedDate = null;
            SelectedType = null;
            Topic = null;
            StartTime = null;
            EndTime = null;
        }

        private void ClearFormErrors()
        {
            ClearErrors(nameof(SelectedDate));
            ClearErrors(nameof(SelectedType));
            ClearErrors(nameof(Topic));
            ClearErrors(nameof(StartTime));
            ClearErrors(nameof(EndTime));
        }

        private void ValidateAddMeetingForm()
        {
            ClearFormErrors();

            if (shouldValidateAddMeetingForm)
            {
                if (!SelectedDate.HasValue)
                {
                    AddError(nameof(SelectedDate), "Date is required.");
                }

                if (string.IsNullOrEmpty(SelectedType))
                {
                    AddError(nameof(SelectedType), "Meeting type is required.");
                }

                if (string.IsNullOrEmpty(Topic))
                {
                    AddError(nameof(Topic), "Topic is required.");
                }

                if (!StartTime.HasValue)
                {
                    AddError(nameof(StartTime), "Start time is required.");
                }

                if (!EndTime.HasValue)
                {
                    AddError(nameof(EndTime), "End time is required.");
                }
                else if (EndTime <= StartTime)
                {
                    AddError(nameof(EndTime), "End time must be later than Start time.");
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
