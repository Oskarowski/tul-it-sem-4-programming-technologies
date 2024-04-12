using DataLayer.API;

namespace DataLayer.Implementations
{
    public class DataContext : IDataContext
    {
        public List<IUser> Users { get; set; }
        public List<IProduct> Products { get; set; }
        public List<IEvent> Events { get; set; }
        public List<IState> States { get; set; }

        public DataContext()
        {
            Users = new List<IUser>();
            Products = new List<IProduct>();
            Events = new List<IEvent>();
            States = new List<IState>();
        }

    }
}