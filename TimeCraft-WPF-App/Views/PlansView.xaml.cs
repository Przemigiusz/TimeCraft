using System.Windows.Controls;
using TimeCraft_WPF_App.ViewModels;

namespace TimeCraft_WPF_App.Views
{
    public partial class PlansView : UserControl
    {
        public PlansView()
        {
            InitializeComponent();
            DataContext = new PlansViewModel();
        }
    }
}
