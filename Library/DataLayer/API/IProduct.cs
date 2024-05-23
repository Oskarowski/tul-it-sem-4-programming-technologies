namespace DataLayer.API
{
    public interface IProduct
    {
        string Guid { get; set; }
        string Name { get; set; }
        double Price { get; set; }
    }
}