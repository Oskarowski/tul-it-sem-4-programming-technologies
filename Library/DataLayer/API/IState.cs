namespace DataLayer.API
{
    public interface IState
    {
        IProduct Product { get; set; }
        int Quantity { get; set; }
        DateTime Date { get; set; }
        double Price { get; set; }
        string Guid { get; }
    }
}
