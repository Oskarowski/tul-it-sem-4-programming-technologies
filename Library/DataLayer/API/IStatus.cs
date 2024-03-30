namespace DataLayer.API
{
    public interface IStatus
    {
        IProduct Product { get; set; }
        int Quantity { get; set; }
        DateTime Date { get; set; }
        double Price { get; set; }
    }
}
