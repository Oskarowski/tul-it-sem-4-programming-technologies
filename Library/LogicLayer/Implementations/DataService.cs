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

        #region Get
        public IUser GetUser(string guid)
        {
            return DataRepository.GetUser(guid);
        }
        public IProduct GetProduct(string guid)
        {
            return DataRepository.GetProduct(guid);
        }
        public IState GetState(string guid)
        {
            return DataRepository.GetState(guid);
        }
        public IEvent GetEvent(string guid)
        {
            return DataRepository.GetEvent(guid);
        }
        #endregion

        #region GetAll
        public List<IUser> GetAllUsers()
        {
            return DataRepository.GetAllUsers();
        }
        public List<IProduct> GetAllProducts()
        {
            return DataRepository.GetAllProducts();
        }
        public List<IState> GetAllStates()
        {
            return DataRepository.GetAllStates();
        }
        #endregion

        #region GetBy
        public List<IEvent> GetEventsByUser(string userGuid)
        {
            return DataRepository.GetEventsByUser(userGuid);
        }

        public List<IEvent> GetEventsByProduct(string productGuid)
        {
            return DataRepository.GetEventsByProduct(productGuid);
        }

        public IProduct GetProductByState(string stateGuid)
        {
            return DataRepository.GetProductByState(stateGuid);
        }

        public List<IEvent> GetEventsByState(string stateGuid)
        {
            return DataRepository.GetEventsByState(stateGuid);
        }
        #endregion

        #region Create
        public IUser CreateUser(string guid, string firstName, string lastName, string email, int phoneNumber)
        {
            return DataRepository.CreateUser(guid, firstName, lastName, email, phoneNumber);
        }

        public IProduct CreateBook(string name, string guid, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            return DataRepository.CreateBook(name, guid, price, author, publisher, pages, publicationDate);
        }

        public IState CreateState(IProduct product, int quantity, DateTime date, double price, string guid)
        {
            return DataRepository.CreateState(product, quantity, date, price, guid);
        }

        public IEvent CreateBorrow(IUser user, IState state, DateTime date, string guid)
        {
            return DataRepository.CreateBorrow(user, state, date, guid);
        }

        public IEvent CreateReturn(IUser user, IState state, DateTime date, string guid)
        {
            return DataRepository.CreateReturn(user, state, date, guid);
        }
        #endregion

        #region Remove
        public void RemoveUser(string guid)
        {
            DataRepository.RemoveUser(guid);
        }

        public void RemoveProduct(string guid)
        {
            DataRepository.RemoveProduct(guid);
        }

        public void RemoveState(string guid)
        {
            DataRepository.RemoveState(guid);
        }

        public void RemoveEvent(string guid)
        {
            DataRepository.RemoveEvent(guid);
        }
        #endregion
    }
}