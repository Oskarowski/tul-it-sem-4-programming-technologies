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
        public IState GetState(string guid)
        {
            return DataRepository.GetState(guid);
        }
        public List<IState> GetAllStates()
        {
            return DataRepository.GetAllStates();
        }
        #endregion

        public List<IEvent> getEventsByUser(string userGuid)
        {
            return DataRepository.getEventsByUser(userGuid);
        }
    }
}