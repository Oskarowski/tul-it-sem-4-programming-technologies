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
    }
}