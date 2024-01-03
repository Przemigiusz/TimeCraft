namespace SharedLibrary.Models
{
    public class User
    {
        private static int userId = 0;
        private int id;
        private string firstName;
        private string lastName;
        private string email;
        private string password;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

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

        public User(string firstName, string lastName, string email, string password)
        {
            this.id = userId;
            ++userId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
        }
    }
}
