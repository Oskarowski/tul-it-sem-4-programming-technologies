using DataLayer.API;
using DataLayer.Database;

namespace DataLayer.Implementations
{
    public class DataRepository : IDataRepository
    {
        private IDataContext _dataContext;

        public static IDataRepository NewInstance(IDataContext? dataContext = null)
        {
            return new DataRepository(dataContext ?? DataContext.NewInstance());
        }

        public DataRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Seed(IDataFiller dataSeeder)
        {
            foreach (IUser user in dataSeeder.GetGeneratedUsers())
            {
                // TODO
            }
            foreach (IState state in dataSeeder.GetGeneratedStates())
            {
                // TODO
            }
            foreach (IProduct product in dataSeeder.GetGeneratedProducts())
            {
                // TODO
            }
            foreach (IEvent @event in dataSeeder.GetGeneratedEvents())
            {
                // TODO
            }
        }

        #region User CRUD

        public async Task AddUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            IUser user = new User(guid, firstName, lastName, email, balance, phoneNumber);

            await _dataContext.AddUserAsync(user);
        }

        public async Task<IUser> GetUserAsync(string guid)
        {
            IUser? user = await _dataContext.GetUserAsync(guid) ?? throw new Exception("This user does not exist!");

            return user;
        }

        public async Task UpdateUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            IUser user = new User(guid, firstName, lastName, email, balance, phoneNumber);

            if (!await CheckIfUserExists(user.Guid))
                throw new Exception("This user does not exist");

            await _dataContext.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(string guid)
        {
            if (!await CheckIfUserExists(guid))
                throw new Exception("This user does not exist");

            await _dataContext.DeleteUserAsync(guid);
        }

        public async Task<Dictionary<string, IUser>> GetAllUsersAsync()
        {
            return await _dataContext.GetAllUsersAsync();
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await _dataContext.GetUsersCountAsync();
        }

        #endregion


        #region Product CRUD

        public async Task AddProductAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            IBook product = new Book(guid, name, price, author, publisher, pages, publicationDate);

            await _dataContext.AddProductAsync(product);
        }

        public async Task<IBook> GetProductAsync(string guid)
        {
            IBook? product = await _dataContext.GetProductAsync(guid);

            if (product is null)
                throw new Exception("This product does not exist!");

            return product;
        }

        public async Task UpdateProductAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            IBook product = new Book(guid, name, price, author, publisher, pages, publicationDate);

            if (!await CheckIfProductExists(product.Guid))
                throw new Exception("This product does not exist");

            await _dataContext.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(string guid)
        {
            if (!await CheckIfProductExists(guid))
                throw new Exception("This product does not exist");

            await _dataContext.DeleteProductAsync(guid);
        }

        public async Task<Dictionary<string, IBook>> GetAllProductsAsync()
        {
            return await _dataContext.GetAllProductsAsync();
        }

        public async Task<int> GetProductsCountAsync()
        {
            return await _dataContext.GetProductsCountAsync();
        }

        #endregion


        #region State CRUD

        public async Task AddStateAsync(string guid, string productGuid, int quantity)
        {
            if (!await _dataContext.CheckIfProductExists(productGuid))
                throw new Exception("This product does not exist!");

            if (quantity < 0)
                throw new Exception("Product's quantity must be number greater that 0!");

            IState state = new State(guid, productGuid, quantity);

            await _dataContext.AddStateAsync(state);
        }

        public async Task<IState> GetStateAsync(string guid)
        {
            IState? state = await _dataContext.GetStateAsync(guid);

            if (state is null)
                throw new Exception("This state does not exist!");

            return state;
        }

        public async Task UpdateStateAsync(string guid, string productGuid, int quantity)
        {
            if (!await _dataContext.CheckIfProductExists(productGuid))
                throw new Exception("This product does not exist!");

            if (quantity <= 0)
                throw new Exception("Product's quantity must be number greater that 0!");

            IState state = new State(guid, productGuid, quantity);

            if (!await CheckIfStateExists(state.Guid))
                throw new Exception("This state does not exist");

            await _dataContext.UpdateStateAsync(state);
        }

        public async Task DeleteStateAsync(string guid)
        {
            if (!await CheckIfStateExists(guid))
                throw new Exception("This state does not exist");

            await _dataContext.DeleteStateAsync(guid);
        }

        public async Task<Dictionary<string, IState>> GetAllStatesAsync()
        {
            return await _dataContext.GetAllStatesAsync();
        }

        public async Task<int> GetStatesCountAsync()
        {
            return await _dataContext.GetStatesCountAsync();
        }

        #endregion


        #region Event CRUD

        public async Task AddEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            IUser user = await GetUserAsync(userGuid);
            IState state = await GetStateAsync(stateGuid);
            IProduct product = await GetProductAsync(state.ProductGuid);

            IEvent newEvent = new Event( guid,  stateGuid,  userGuid,  createdAt,  type);

            switch (type)
            {
                case "RentEvent":
                    if (state.Quantity == 0)
                        throw new Exception("Such Product can't be rented!");

                    await UpdateStateAsync(stateGuid, product.Guid, state.Quantity - 1);
                    await UpdateUserAsync(userGuid, user.FirstName, user.LastName, user.Email, user.Balance, user.PhoneNumber);

                    break;

                case "ReturnEvent":
                    Dictionary<string, IEvent> events = await GetAllEventsAsync();
                    Dictionary<string, IState> states = await GetAllStatesAsync();

                    int copiesBought = 0;

                    foreach
                    (
                        IEvent even in
                        from evennt in events.Values
                        from statee in states.Values
                        where evennt.UserGuid == user.Guid &&
                              evennt.StateGuid == statee.Guid &&
                              statee.ProductGuid == product.Guid
                        select evennt
                    )
                        if (even.Type == "PurchaseEvent")
                            copiesBought++;
                        else if (even.Type == "ReturnEvent")
                            copiesBought--;

                    copiesBought--;

                    if (copiesBought < 0)
                    {
                        throw new Exception("You do not own this product!");
                    }

                    await UpdateStateAsync(stateGuid, product.Guid, state.Quantity + 1);

                    break;
                case "SupplyEvent":

                    await this.UpdateStateAsync(stateGuid, product.Guid, state.Quantity + 1);

                    break;

                default:
                    throw new Exception("This Event Type is not handled");
            }

            await _dataContext.AddEventAsync(newEvent);
        }

        public async Task<IEvent> GetEventAsync(string guid)
        {
            IEvent? even = await _dataContext.GetEventAsync(guid);

            return even is null ? throw new Exception("This event does not exist!") : even;
        }

        public async Task UpdateEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            IEvent newEvent = new Event(guid, stateGuid, userGuid, createdAt, type);

            if (!await CheckIfEventExists(newEvent.Guid, type))
                throw new Exception("This event does not exist");

            await _dataContext.UpdateEventAsync(newEvent);
        }

        public async Task DeleteEventAsync(string guid)
        {
            if (!await CheckIfEventExists(guid, "RentEvent"))
                throw new Exception("This event does not exist");

            await _dataContext.DeleteEventAsync(guid);
        }

        public async Task<Dictionary<string, IEvent>> GetAllEventsAsync()
        {
            return await _dataContext.GetAllEventsAsync();
        }

        public async Task<int> GetEventsCountAsync()
        {
            return await _dataContext.GetEventsCountAsync();
        }

        #endregion


        #region Utils

        public async Task<bool> CheckIfUserExists(string guid)
        {
            return await _dataContext.CheckIfUserExists(guid);
        }

        public async Task<bool> CheckIfProductExists(string guid)
        {
            return await _dataContext.CheckIfProductExists(guid);
        }

        public async Task<bool> CheckIfStateExists(string guid)
        {
            return await _dataContext.CheckIfStateExists(guid);
        }

        public async Task<bool> CheckIfEventExists(string guid, string type)
        {
            return await _dataContext.CheckIfEventExists(guid, type);
        }
        #endregion
    }
}