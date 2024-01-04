using SharedLibrary.Models;

namespace SharedLibrary.Repositories
{
    public class UserSession
    {
        private static UserSession? instance;

        public static UserSession Instance
        {
            get
            {
                instance ??= new UserSession();
                return instance;
            }
        }

        private User? loggedUser;

        public User? LoggedUser
        {
            get { return loggedUser; }
            set { loggedUser = value; }
        }

        private UserSession() { }
    }
}
