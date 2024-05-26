namespace PresentationLayer.Model.API
{
    public interface IProductModel
    {
        string Guid { get; set; }
        string Name { get; set; }
        double Price { get; set; }
        string Author { get; set; }
        string Publisher { get; set; }
        int Pages { get; set; }
        DateTime PublicationDate { get; set; }
    }
}
