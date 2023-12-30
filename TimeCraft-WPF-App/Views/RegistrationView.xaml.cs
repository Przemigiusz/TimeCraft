using System.Windows.Controls;
using TimeCraft_WPF_App.ViewModels;


namespace TimeCraft_WPF_App.Views
{
    public partial class RegistrationView : Page
    {
        public RegistrationView()
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel();
        }
    }
}
