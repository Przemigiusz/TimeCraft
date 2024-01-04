namespace SharedLibrary.Models
{
    public class Login
    {
        private string email;
        private string password;

        public Login(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public string Email { get { return email; } set { email = value; } }
        public string Password { get { return password; } set { password = value; } }
    }
}
