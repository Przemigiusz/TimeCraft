using System.Windows.Input;
using TimeCraft_WPF_App.Core;
using SharedLibrary.Services;

namespace TimeCraft_WPF_App.ViewModels
{
    class RegistrationViewModel : ViewModelBase
    {
        private string? firstName;
        private string? lastName;
        private string? email;
        private string? password;
        private string? repeatPassword;

        private UsersService usersService;

        public ICommand SignUpCommand { get; set; }

        public RegistrationViewModel()
        {
            usersService = UsersService.Instance;
            SignUpCommand = new RelayCommand(_ => SignUp(), _ => CanSignUp());
        }

        private bool CanSignUp()
        {
            return !string.IsNullOrEmpty(FirstName)
                && !string.IsNullOrEmpty(LastName)
                && !string.IsNullOrEmpty(Email)
                && !string.IsNullOrEmpty(Password)
                && !string.IsNullOrEmpty(RepeatPassword)
                && (Password == RepeatPassword);
        }

        private void SignUp()
        {
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(RepeatPassword) && (Password == RepeatPassword))
            {

            }
        }

        public string? FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string? LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
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

        public string? RepeatPassword
        {
            get { return repeatPassword; }
            set
            {
                if (repeatPassword != value)
                {
                    repeatPassword = value;
                    OnPropertyChanged(nameof(RepeatPassword));
                }
            }
        }
    }
}
