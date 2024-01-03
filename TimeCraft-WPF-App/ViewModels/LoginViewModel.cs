using System.Windows.Input;
using TimeCraft_WPF_App.Core;
using SharedLibrary.Services;

namespace TimeCraft_WPF_App.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        private string? email;
        private string? password;

        private UsersService usersService;

        public ICommand SignInCommand { get; set; }

        public LoginViewModel()
        {
            usersService = UsersService.Instance;
            SignInCommand = new RelayCommand(_ => SignIn(), _ => CanSignIn());
        }


        private bool CanSignIn()
        {
            return !string.IsNullOrEmpty(Email)
                && !string.IsNullOrEmpty(Password);
        }

        private void SignIn()
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {

            }
        }

        public string? Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string? Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
    }
}
