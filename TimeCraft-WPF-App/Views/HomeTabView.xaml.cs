using System.Windows.Controls;
using TimeCraft_WPF_App.ViewModels;

namespace TimeCraft_WPF_App.Views
{
    public partial class HomeTabView : UserControl
    {
        public HomeTabView()
        {
            InitializeComponent();
            DataContext = new HomeTabViewModel(); ;
        }
    }
}
