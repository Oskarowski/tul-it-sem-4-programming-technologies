using DataLayer.API;
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
            return _dataRepository.GetUser(guid);
        }
        public IProduct GetProduct(string guid)
        {
            return _dataRepository.GetProduct(guid);
        }
        public IState GetState(string guid)
        {
            return _dataRepository.GetState(guid);
        }
        public IEvent GetEvent(string guid)
        {
            return _dataRepository.GetEvent(guid);
        }
        #endregion

        #region GetAll
        public List<IUser> GetAllUsers()
        {
            return _dataRepository.GetAllUsers();
        }
        public List<IProduct> GetAllProducts()
        {
            return _dataRepository.GetAllProducts();
        }
        public List<IState> GetAllStates()
        {
            return _dataRepository.GetAllStates();
        }
        public List<IEvent> GetAllEvents()
        {
            return _dataRepository.GetAllEvents();
        }
        #endregion

        #region GetBy
        public List<IEvent> GetEventsByUser(string userGuid)
        {
            return _dataRepository.GetEventsByUser(userGuid);
        }
        public List<IEvent> GetEventsByProduct(string productGuid)
        {
            return _dataRepository.GetEventsByProduct(productGuid);
        }
        public IProduct GetProductByState(string stateGuid)
        {
            return _dataRepository.GetProductByState(stateGuid);
        }
        public List<IEvent> GetEventsByState(string stateGuid)
        {
            return _dataRepository.GetEventsByState(stateGuid);
        }
        #endregion

        // #region Create
        // public IUser CreateUser(string guid, string firstName, string lastName, string email, int phoneNumber)
        // {
        //     IUser user = new User(guid, firstName, lastName, email, phoneNumber);
        //     return _dataRepository.AddUser(new User(guid, firstName, lastName, email, phoneNumber));
        // }

        // public IProduct CreateBook(string name, string guid, double price, string author, string publisher, int pages, DateTime publicationDate)
        // {
        //     return _dataRepository.CreateBook(name, guid, price, author, publisher, pages, publicationDate);
        // }

        // public IState CreateState(IProduct product, int quantity, DateTime date, double price, string guid)
        // {
        //     return _dataRepository.CreateState(product, quantity, date, price, guid);
        // }

        // public IEvent CreateBorrow(IUser user, IState state, DateTime date, string guid)
        // {
        //     return _dataRepository.CreateBorrow(user, state, date, guid);
        // }

        // public IEvent CreateReturn(IUser user, IState state, DateTime date, string guid)
        // {
        //     return _dataRepository.CreateReturn(user, state, date, guid);
        // }
        // #endregion

        #region Remove
        public void RemoveUser(string guid)
        {
            _dataRepository.RemoveUser(guid);
        }
        public void RemoveProduct(string guid)
        {
            _dataRepository.RemoveProduct(guid);
        }
        public void RemoveState(string guid)
        {
            _dataRepository.RemoveState(guid);
        }
        public void RemoveEvent(string guid)
        {
            _dataRepository.RemoveEvent(guid);
        }
        #endregion
    }
}