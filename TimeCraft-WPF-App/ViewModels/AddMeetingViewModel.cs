using SharedLibrary.Models;
using SharedLibrary.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using System.Xml.Linq;
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

        public ICommand AddMeetingCommand { get; set; }

        public AddMeetingViewModel()
        {
            plansService = PlansService.Instance;
            kindsOfMeetings = new ObservableCollection<string>(plansService.PlansRepository.KindsOfMeetings);
            AddMeetingCommand = new RelayCommand(_ => AddMeeting(), _ => CanAddMeeting());
        }

        private bool CanAddMeeting()
        {
            return SelectedDate.HasValue
                && !string.IsNullOrEmpty(SelectedType)
                && !string.IsNullOrEmpty(Topic)
                && StartTime.HasValue
                && EndTime.HasValue;
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

        private void AddMeeting()
        {
            if (SelectedDate.HasValue && !string.IsNullOrEmpty(SelectedType) && !string.IsNullOrEmpty(Topic) && StartTime.HasValue && EndTime.HasValue)
            {
                string formattedDate = SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                string formattedStartTime = StartTime.Value.ToString("HH:mm", CultureInfo.InvariantCulture);
                string formattedEndTime = EndTime.Value.ToString("HH:mm", CultureInfo.InvariantCulture);

                Meeting newMeeting = new Meeting(SelectedType, Topic, formattedDate, formattedStartTime, formattedEndTime);
                plansService.PlansRepository.addMeeting(newMeeting);

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
}
}
