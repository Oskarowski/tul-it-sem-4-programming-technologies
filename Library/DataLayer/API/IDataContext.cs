namespace DataLayer.API
{
    public interface IDataContext
    {
        public List<IUser> Users { get; set; }

        public List<IProduct> Products { get; set; }

        public List<IEvent> Events { get; set; }

        public List<IState> States { get; set; }
    }
}