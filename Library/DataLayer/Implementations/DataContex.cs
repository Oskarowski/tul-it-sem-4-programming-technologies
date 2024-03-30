using DataLayer.API;

namespace DataLayer.Implementations
{
    public class DataContext : IDataContext
    {
        public List<IUser> Users { get; set; }
        public List<IProduct> Products { get; set; }

        public DataContext()
        {
            Users = new List<IUser>();
            Products = new List<IProduct>();
        }

    }
}