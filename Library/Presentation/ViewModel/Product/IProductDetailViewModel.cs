using Presentation.Model.API;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public interface IProductDetailViewModel
    {
        static IProductDetailViewModel CreateViewModel(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate, IProductModelOperation model)
        {
            return new ProductDetailViewModel(guid, name, price, author, publisher, pages, publicationDate, model);
        }

        ICommand UpdateProduct { get; set; }

        public string Guid { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Pages { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
