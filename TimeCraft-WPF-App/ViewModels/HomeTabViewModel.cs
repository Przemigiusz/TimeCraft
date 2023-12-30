using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace TimeCraft_WPF_App.ViewModels
{
    public class HomeTabViewModel : ViewModelBase
    {
        private string? _greetingP1;
        private string? _greetingP2;
        private string? _currentTime;
        private Timer _timer;

        public string? GreetingP1
        {
            get { return _greetingP1; }
            set
            {
                if (_greetingP1 != value)
                {
                    _greetingP1 = value;
                    OnPropertyChanged(nameof(GreetingP1));
                }
            }
        }

        public string? GreetingP2
        {
            get { return _greetingP2; }
            set
            {
                if (_greetingP2 != value)
                {
                    _greetingP2 = value;
                    OnPropertyChanged(nameof(GreetingP2));
                }
            }
        }

        public string? CurrentTime
        {
            get { return _currentTime; }
            set
            {
                if (_currentTime != value)
                {
                    _currentTime = value;
                    OnPropertyChanged(nameof(CurrentTime));
                }
            }
        }

        public HomeTabViewModel()
        {
            GreetingP1 = "Hello,";
            GreetingP2 = "What's Up Today?";

            _timer = new Timer(TimerCallback, null, 0, 1000);
        }

        private void TimerCallback(object? state)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            });
        }
    }
}
