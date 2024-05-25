using DataLayer.Implementations;

namespace DataLayer.API
{
    public interface IDataContext
    {
        static IDataContext NewInstance(string? connectionString = null) {
            return new DataContext(connectionString);
        }

        #region User CRUD

        Task AddUserAsync(IUser user);
        Task<IUser?> GetUserAsync(string guid);
        Task UpdateUserAsync(IUser user);
        Task DeleteUserAsync(string guid);
        Task<Dictionary<string, IUser>> GetAllUsersAsync();
        Task<int> GetUsersCountAsync();

        #endregion User CRUD

        #region Product CRUD

        Task AddProductAsync(IBook product);
        Task<IBook?> GetProductAsync(string guid);
        Task UpdateProductAsync(IBook product);
        Task DeleteProductAsync(string guid);
        Task<Dictionary<string, IBook>> GetAllProductsAsync();
        Task<int> GetProductsCountAsync();

        #endregion

        #region State CRUD

        Task AddStateAsync(IState state);
        Task<IState?> GetStateAsync(string guid);
        Task UpdateStateAsync(IState state);
        Task DeleteStateAsync(string guid);
        Task<Dictionary<string, IState>> GetAllStatesAsync();
        Task<int> GetStatesCountAsync();

        #endregion

        #region Event CRUD
        
        Task AddEventAsync(IEvent even);
        Task<IEvent?> GetEventAsync(string guid);
        Task UpdateEventAsync(IEvent even);
        Task DeleteEventAsync(string guid);
        Task<Dictionary<string, IEvent>> GetAllEventsAsync();
        Task<int> GetEventsCountAsync();

        #endregion

        #region Helpers 

        Task<bool> CheckIfUserExists(string guid);
        Task<bool> CheckIfProductExists(string guid);
        Task<bool> CheckIfStateExists(string guid);
        Task<bool> CheckIfEventExists(string guid, string type);

        #endregion
    }
}