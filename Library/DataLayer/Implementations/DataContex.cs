using DataLayer.API;

namespace DataLayer.Implementations
{
    public class DataContext : IDataContext
    {
        private DataContext()
        {
            Users = new List<IUser>();
            Products = new List<IProduct>();
            Events = new List<IEvent>();
            States = new List<IState>();
        }
        private DataContext(IDataFiller filler)
        {
            Users = new List<IUser>();
            Products = new List<IProduct>();
            Events = new List<IEvent>();
            States = new List<IState>();
            
            filler.Fill(this);
        }
        public static IDataContext createDataContext()
        {
            return new DataContext();
        }
        public static IDataContext createDataContext(IDataFiller filler)
        {
            return new DataContext(filler);
        }
        
        public List<IUser> Users { get; set; }
        public List<IProduct> Products { get; set; }
        public List<IEvent> Events { get; set; }
        public List<IState> States { get; set; }
    }
}