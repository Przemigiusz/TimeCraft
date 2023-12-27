using System.Windows.Controls;
using TimeCraft_WPF_App.ViewModels;

namespace TimeCraft_WPF_App.Views
{
    public partial class AddTaskView : UserControl
    {
        public AddTaskView()
        {
            InitializeComponent();
            DataContext = new AddTaskViewModel();
        }
    }
}
