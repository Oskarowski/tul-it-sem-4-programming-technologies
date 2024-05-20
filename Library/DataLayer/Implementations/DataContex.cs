using DataLayer.API;
using System.Collections.Generic;

namespace DataLayer.Implementations
{
    public class DataContext : IDataContext
    {
        public static IDataContext NewInstance()
        {
            return new DataContext();
        }

        public List<IUser> Users { get; set; }
        public List<IProduct> Products { get; set; }
        public List<IEvent> Events { get; set; }
        public List<IState> States { get; set; }

        private DataContext()
        {
            Users = new List<IUser>();
            Products = new List<IProduct>();
            Events = new List<IEvent>();
            States = new List<IState>();
        }

    }
}