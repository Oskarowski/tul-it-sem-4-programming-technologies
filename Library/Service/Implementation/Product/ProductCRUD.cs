using DataLayer.API;
using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ProductCRUD : IProductCRUD
    {
        private readonly IDataRepository _dataRepository;

        public ProductCRUD(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task AddProductAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            await _dataRepository.AddProductAsync(guid, name, price, author, publisher, pages, publicationDate);
        }

        public IProductDTO Map(IBook book)
        {
            return new ProductDTO(book.Guid, book.Name, book.Price, book.Author, book.Publisher, book.Pages, book.PublicationDate);
        }

        public async Task AddBookAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            await _dataRepository.AddProductAsync(guid, name, price, author, publisher, pages, publicationDate);
        }

        public async Task<IProductDTO> GetProductAsync(string guid)
        {
            return Map(await _dataRepository.GetProductAsync(guid));
        }

        public async Task UpdateProductAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            await _dataRepository.UpdateProductAsync(guid, name, price, author, publisher, pages, publicationDate);
        }

        public async Task DeleteProductAsync(string guid)
        {
            await _dataRepository.DeleteProductAsync(guid);
        }

        public async Task<Dictionary<string, IProductDTO>> GetAllProductsAsync()
        {
            Dictionary<string, IProductDTO> result = new Dictionary<string, IProductDTO>();

            foreach (IBook book in (await _dataRepository.GetAllProductsAsync()).Values)
            {
                result.Add(book.Guid, this.Map(book));
            }

            return result;
        }

        public async Task<int> GetProductsCountAsync()
        {
            return await _dataRepository.GetProductsCountAsync();
        }
    }
}
