using DataLayer.Implementations;

namespace DataLayer.API
{
    public interface IDataRepository
    {
        static IDataRepository NewInstance(IDataContext? dataContext = null)
        {
            return new DataRepository(dataContext ?? DataContext.NewInstance());
        }

        public void Seed(IDataFiller dataSeeder);

        #region User CRUD

        Task AddUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber);
        Task<IUser> GetUserAsync(string guid);
        Task UpdateUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber);
        Task DeleteUserAsync(string guid);
        Task<Dictionary<string, IUser>> GetAllUsersAsync();
        Task<int> GetUsersCountAsync();
        
        #endregion User CRUD


        #region Product CRUD

        Task AddProductAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate);
        Task<IBook> GetProductAsync(string guid);
        Task UpdateProductAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate);
        Task DeleteProductAsync(string guid);
        Task<Dictionary<string, IBook>> GetAllProductsAsync();
        Task<int> GetProductsCountAsync();

        #endregion


        #region State CRUD

        Task AddStateAsync(string guid, string productGuid, int quantity);
        Task<IState> GetStateAsync(string guid);
        Task UpdateStateAsync(string guid, string productGuid, int quantity);
        Task DeleteStateAsync(string guid);
        Task<Dictionary<string, IState>> GetAllStatesAsync();
        Task<int> GetStatesCountAsync();

        #endregion


        #region Event CRUD

        Task AddEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type);
        Task<IEvent> GetEventAsync(string guid);
        Task UpdateEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type);
        Task DeleteEventAsync(string guid);
        Task<Dictionary<string, IEvent>> GetAllEventsAsync();
        Task<int> GetEventsCountAsync();

        #endregion
    }
}