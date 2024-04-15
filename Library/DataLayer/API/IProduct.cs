namespace DataLayer.API
{
    public interface IProduct
    {
        string Guid { get; }
        string Name { get; set; }
        double Price { get; set; }
    }
}