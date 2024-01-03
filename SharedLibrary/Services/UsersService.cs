using SharedLibrary.Repositories;

namespace SharedLibrary.Services
{
    public class UsersService
    {
        private static UsersService? instance;
        private UsersRepository usersRepository;
        public UsersRepository UsersRepository { get { return usersRepository; } }

        private UsersService()
        {
            this.usersRepository = UsersRepository.Instance;
        }
        public static UsersService Instance
        {
            get
            {
                instance ??= new UsersService();
                return instance;
            }
        }
    }
}
