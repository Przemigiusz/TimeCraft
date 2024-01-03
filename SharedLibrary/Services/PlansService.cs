using SharedLibrary.Repositories;

namespace SharedLibrary.Services
{
    public class PlansService
    {
        private static PlansService? instance;
        private PlansRepository plansRepository;
        public PlansRepository PlansRepository { get { return plansRepository; } }

        private PlansService()
        {
            this.plansRepository = PlansRepository.Instance;
        }
        public static PlansService Instance
        {
            get
            {
                instance ??= new PlansService();
                return instance;
            }
        }
    }
}
