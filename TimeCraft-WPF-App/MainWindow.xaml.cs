using System.Windows;
using TimeCraft_WPF_App.ViewModels;

namespace TimeCraft_WPF_App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
