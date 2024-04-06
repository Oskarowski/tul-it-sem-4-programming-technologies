using LogicLayer.API;

namespace LogicLayer.Implementations
{
    public class DataService : IDataService
    {
        private IDataRepository _dataRepository;
        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        #region User
        public void AddUser(IUser user)
        {
            DataRepository.AddUser(user);
        }
        public IUser GetUser(string guid)
        {
            return DataRepository.GetUser(guid);
        }
        public List<IUser> GetAllUsers()
        {
            return DataRepository.GetAllUsers();
        }
        #endregion

        #region Product
        public void AddProduct(IProduct product)
        {
            DataRepository.AddProduct(product);
        }
        public IProduct GetProduct(string guid)
        {
            return DataRepository.GetProduct(guid);
        }
        public List<IProduct> GetAllProducts()
        {
            return DataRepository.GetAllProducts();
        }
        #endregion

        #region State
        public void AddState(IState state)
        {
            DataRepository.AddState(state);
        }
        public IState GetState(string guid)
        {
            return DataRepository.GetState(guid);
        }
        #endregion
    }
}