namespace DataLayer.API
{
    public interface IDataFiller
    {
        public List<IUser> GetGeneratedUsers();
        public List<IProduct> GetGeneratedProducts();
        public List<IEvent> GetGeneratedEvents();
        public List<IState> GetGeneratedStates();
    }
}