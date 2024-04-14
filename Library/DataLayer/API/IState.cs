namespace DataLayer.API
{
    public interface IState
    {
        IProduct Product { get; set; }
        int Quantity { get; set; }
        DateTime LastUpdatedDate { get; }
        string Guid { get; }
    }
}
