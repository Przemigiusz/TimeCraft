namespace SharedLibrary.Models
{
    public class RegisteredUser
    {
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private string repeatedPassword;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string RepeatedPassword
        {
            get { return repeatedPassword; }
            set { repeatedPassword = value; }
        }

        public RegisteredUser(string firstName, string lastName, string email, string password, string repeatedPassword)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
            this.repeatedPassword = repeatedPassword;
        }
    }
}
