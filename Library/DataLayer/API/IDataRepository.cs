using DataLayer.Implementations;

namespace DataLayer.API
{
    public interface IDataRepository
    {
        static IDataRepository CreateDataRepository(IDataContext dataContext)
        {
            return new DataRepository(dataContext);
        }

        #region User
        void AddUser(IUser user);
        IUser GetUser(string guid);
        List<IUser> GetAllUsers();
        #endregion

        #region Product
        void AddProduct(IProduct product);
        IProduct GetProduct(string guid);
        List<IProduct> GetAllProducts();
        #endregion

        #region Event
        void AddEvent(IEvent @event);
        IEvent GetEvent(string guid);
        List<IEvent> GetAllEvents();
        #endregion

        #region State
        void AddState(IState state);
        IState GetState(string guid);
        List<IState> GetAllStates();
        #endregion
    }
}