namespace DataLayer.API
{
    public interface IDataContext
    {
        public List<IUser> Users { get; set; }

        public List<IProduct> Products { get; set; }
    }
}