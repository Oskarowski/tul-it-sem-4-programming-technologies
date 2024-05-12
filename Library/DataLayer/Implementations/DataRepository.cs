using DataLayer.API;
using DataLayer.Implementations.Events;

namespace DataLayer.Implementations
{
    public class DataRepository : IDataRepository
    {
        private IDataContext _dataContext;

        public static IDataRepository NewInstance(IDataContext dataContext)
        {
            return new DataRepository(dataContext);
        }
        private DataRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Seed(IDataFiller dataSeeder)
        {
            foreach (IUser user in dataSeeder.GetGeneratedUsers())
            {
                AddUser(user);
            }
            foreach (IState state in dataSeeder.GetGeneratedStates())
            {
                AddState(state);
            }
            foreach (IProduct product in dataSeeder.GetGeneratedProducts())
            {
                AddProduct(product);
            }
            foreach (IEvent @event in dataSeeder.GetGeneratedEvents())
            {
                AddEvent(@event);
            }
        }

        #region User
        public void AddUser(IUser user)
        {
            _dataContext.Users.Add(user);
        }
        public IUser GetUser(string guid)
        {
            IUser? user = _dataContext.Users.FirstOrDefault(u => u.Guid == guid) ?? throw new Exception("User does not exist");

            return user;
        }
        public List<IUser> GetAllUsers()
        {
            return _dataContext.Users;
        }
        public void RemoveUser(string guid)
        {
            IUser user = _dataContext.Users.FirstOrDefault(user => user.Guid == guid) ?? throw new Exception("User does not exist");

            _dataContext.Users.Remove(user);
        }
        public bool DoesUserExist(string guid)
        {
            return _dataContext.Users.Exists(e => e.Guid == guid);
        }
        public void UpdateUser(IUser updateUser)
        {
            IUser? userToBeUpdated = _dataContext.Users.FirstOrDefault(u => u.Guid == updateUser.Guid);

            if (userToBeUpdated == null)
            {
                throw new Exception("Cannot update user that does not exist");
            }

            userToBeUpdated = updateUser;
        }

        #endregion

        #region Product
        public void AddProduct(IProduct product)
        {
            _dataContext.Products.Add(product);
        }
        public IProduct GetProduct(string guid)
        {
            IProduct product = _dataContext.Products.FirstOrDefault(product => product.Guid == guid) ?? throw new Exception("Product does not exist");

            return product;
        }
        public List<IProduct> GetAllProducts()
        {
            return _dataContext.Products;
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
        public bool DoesProductExist(string guid)
        {
            return _dataContext.Products.Exists(e => e.Guid == guid);
        }
        public void RemoveProduct(string guid)
        {
            IProduct product = _dataContext.Products.FirstOrDefault(product => product.Guid == guid) ?? throw new Exception("Product does not exist");

            _dataContext.Products.Remove(product);
        }
        #endregion

        #region Event
        public void AddEvent(IEvent @event)
        {
            _dataContext.Events.Add(@event);
        }
        public void AddBorrow(IUser user, IState state)
        {
            AddEvent(new Borrow(user, state));
        }
        public void AddDelivery(IUser user, IState state, int amount)
        {
            AddEvent(new Delivery(user, state, amount));
        }
        public void AddReturn(IUser user, IState state)
        {
            AddEvent(new Return(user, state));
        }
        public IEvent GetEvent(string guid)
        {
            IEvent @event = _dataContext.Events.FirstOrDefault(@event => @event.Guid == guid) ?? throw new Exception("Event does not exist");

            return @event;
        }
        public List<IEvent> GetAllEvents()
        {
            return _dataContext.Events;
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
        #endregion

        #region State
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
        #endregion
    }
}