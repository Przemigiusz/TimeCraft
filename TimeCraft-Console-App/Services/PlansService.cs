using TimeCraft_Console_App.Repositories;

namespace TimeCraft_Console_App.Services
{
    internal class PlansService
    {
        private static PlansService? instance;
        private PlansRepository plansRepository;
        public PlansRepository PlansRepository { get { return plansRepository; } }
        private PlansService()
        {
            this.plansRepository = new PlansRepository();
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
