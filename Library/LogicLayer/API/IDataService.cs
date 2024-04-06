namespace LogicLayer.API
{
    public interface IDataService
    {
        #region Find
        IProduct FindProduct(string guid);
        IState FindState(string guid);
        IUser FindUser(string guid);
        IEvent FindEvent(string guid);
        #endregion

        #region Add
        void AddProduct(IProduct product);
        void AddState(IState state);
        void AddUser(IUser user);
        void AddEvent(IEvent @event);
        #endregion

        #region Remove
        void RemoveProduct(string guid);
        void RemoveState(string guid);
        void RemoveUser(string guid);
        void RemoveEvent(string guid);
        #endregion

        #region Create
        IProduct CreateBook(string name, string guid, double price, string author, string publisher, int pages, DateTime publicationDate);
        IEvent CreateBorrow(IUser user, IState state, DateTime date, string guid);
        IEvent CreateReturn(IUser user, IState state, DateTime date, string guid);
        IUser CreateUser(string guid, string firstName, string lastName, string email, int phoneNumber);
        IState CreateState(IProduct product, int quantity, DateTime date, double price, string guid);
        #endregion
    }
}