using Presentation.Model.API;
using Service.API;

namespace Presentation.Model.Implementation
{
    internal class ProductModel : IProductModel
    {
        public ProductModel(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            Guid = string.IsNullOrEmpty(guid) ? System.Guid.NewGuid().ToString() : guid;
            Name = name;
            Price = price;
            Author = author;
            Publisher = publisher;
            Pages = pages;
            PublicationDate = publicationDate;
        }
        public string Guid { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Pages { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
