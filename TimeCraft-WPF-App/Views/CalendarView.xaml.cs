using System.Windows.Controls;
using TimeCraft_WPF_App.ViewModels;

namespace TimeCraft_WPF_App.Views
{
    public partial class CalendarView : UserControl
    {
        public CalendarView(INavigation navigation)
        {
            InitializeComponent();
            DataContext = new CalendarViewModel(navigation);
        }
    }
}
