using DataLayer.Implementations;

namespace DataLayer.API
{
    public interface IDataRepository
    {
        static IDataRepository CreateDataRepository(IDataContext dataContext)
        {
            return new DataRepository(dataContext);
        }

        void AddUser(IUser user);
        IUser GetUser(string guid);
        List<IUser> GetAllUsers();



        void AddProduct(IProduct product);
        IProduct GetProduct(string guid);
        List<IProduct> GetAllProducts();
    }
}