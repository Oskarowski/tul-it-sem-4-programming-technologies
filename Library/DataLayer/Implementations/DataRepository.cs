using DataLayer.API;

namespace DataLayer.Implementations
{
    public class DataRepository : IDataRepository
    {
        private IDataContext _dataContext;

        // Init the data context
        public DataRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // -----> User methods
        public void AddUser(IUser user)
        {
            _dataContext.Users.Add(user);
        }
        public List<IUser> GetAllUsers()
        {
            return _dataContext.Users;
        }
        public IUser GetUser(string guid)
        {
            IUser? user = _dataContext.Users.FirstOrDefault(u => u.Guid == guid) ?? throw new Exception("User does not exist");

            return user;
        }
        public void RemoveUser(string guid)
        {
            IUser user = _dataContext.Users.FirstOrDefault(user => user.Guid == guid) ?? throw new Exception("User does not exist");

            _dataContext.Users.Remove(user);
        }

        // -----> Product methods
        public void AddProduct(IProduct product)
        {
            _dataContext.Products.Add(product);
        }
        public List<IProduct> GetAllProducts()
        {
            return _dataContext.Products;
        }
        public IProduct GetProduct(string guid)
        {
            IProduct product = _dataContext.Products.FirstOrDefault(product => product.Guid == guid) ?? throw new Exception("Product does not exist");

            return product;
        }
        public IProduct GetProductByState(string stateGuid)
        {
            IProduct? product = _dataContext.States.FirstOrDefault(state => state.Guid == stateGuid)?.Product;
            if (product == null)
            {
                throw new Exception("Product does not exist");
            }

            return product;
        }
        public void RemoveProduct(string guid)
        {
            IProduct product = _dataContext.Products.FirstOrDefault(product => product.Guid == guid) ?? throw new Exception("Product does not exist");

            _dataContext.Products.Remove(product);
        }

        // -----> Event methods
        public void AddEvent(IEvent @event)
        {
            _dataContext.Events.Add(@event);
        }
        public List<IEvent> GetAllEvents()
        {
            return _dataContext.Events;
        }
        public IEvent GetEvent(string guid)
        {
            IEvent @event = _dataContext.Events.FirstOrDefault(@event => @event.Guid == guid) ?? throw new Exception("Event does not exist");

            return @event;
        }
        public List<IEvent> GetEventsByUser(string userGuid)
        {
            List<IEvent> events = _dataContext.Events.Where(@event => @event.User.Guid == userGuid).ToList();

            return events;
        }
        public List<IEvent> GetEventsByProduct(string productGuid)
        {
            List<IEvent> events = _dataContext.Events.Where(@event => @event.State.Product.Guid == productGuid).ToList();

            return events;
        }
        public List<IEvent> GetEventsByState(string stateGuid)
        {
            List<IEvent> events = _dataContext.Events.Where(@event => @event.State.Guid == stateGuid).ToList();

            return events;
        }
        public void RemoveEvent(string guid)
        {
            IEvent @event = _dataContext.Events.FirstOrDefault(@event => @event.Guid == guid) ?? throw new Exception("Event does not exist");

            _dataContext.Events.Remove(@event);
        }

        // -----> State methods
        public void AddState(IState state)
        {
            _dataContext.States.Add(state);
        }
        public List<IState> GetAllStates()
        {
            return _dataContext.States;
        }
        public IState GetState(string guid)
        {
            IState state = _dataContext.States.FirstOrDefault(state => state.Guid == guid) ?? throw new Exception("State does not exist");

            return state;
        }
        public void RemoveState(string guid)
        {
            IState state = _dataContext.States.FirstOrDefault(state => state.Guid == guid) ?? throw new Exception("State does not exist");

            _dataContext.States.Remove(state);
        }
    }
}