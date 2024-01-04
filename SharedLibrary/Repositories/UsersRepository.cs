using SharedLibrary.Models;

namespace SharedLibrary.Repositories
{
    public class UsersRepository
    {
        private static UsersRepository? instance;

        private List<User> users;
        public List<User> Users { get { return users; } }

        public static UsersRepository Instance
        {
            get
            {
                instance ??= new UsersRepository();
                return instance;
            }
        }

        private UsersRepository()
        {
            this.users = new List<User>();
            this.initializeUsers();
        }

        private void initializeUsers()
        {
            User user1 = new User("a", "a", "a@o2.pl", "a");
            User user2 = new User("b", "b", "b@o2.pl", "b");
            User user3 = new User("c", "c", "c@o2.pl", "c");

            this.users.Add(user1);
            this.users.Add(user2);
            this.users.Add(user3);
        }

        public void addUser(User user)
        {
            this.users.Add(user);
        }

        public bool userExists(string email, string password)
        {
            foreach (var user in this.users)
            {
                if (user.Email == email && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public User? getUser(string email, string password)
        {
            foreach (var user in this.users)
            {
                if (user.Email == email && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }

        public bool emailExists(string email)
        {
            foreach (var user in this.users)
            {
                if (user.Email == email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
