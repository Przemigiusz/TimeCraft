using System.Windows.Controls;
using TimeCraft_WPF_App.ViewModels;

namespace TimeCraft_WPF_App.Views
{
    public partial class AddMeetingView : UserControl
    {
        public AddMeetingView()
        {
            InitializeComponent();
            DataContext = new AddMeetingViewModel();
        }
    }
}
