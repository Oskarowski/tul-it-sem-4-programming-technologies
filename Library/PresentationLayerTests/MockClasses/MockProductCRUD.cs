using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTests.MockClasses
{
    internal class MockProductCRUD : IProductCRUD
    {
        private MockRepository mockRepository = new MockRepository();

        public async Task AddProductAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            await mockRepository.AddProductAsync(guid, name, price, author, publisher, pages, publicationDate);
        }

        public async Task<IProductDTO> GetProductAsync(string guid)
        {
            return await mockRepository.GetProductAsync(guid);
        }

        public async Task UpdateProductAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            await mockRepository.UpdateProductAsync(guid, name, price, author, publisher, pages, publicationDate);
        }

        public async Task DeleteProductAsync(string guid)
        {
            await mockRepository.DeleteProductAsync(guid);
        }

        public async Task<Dictionary<string, IProductDTO>> GetAllProductsAsync()
        {
            return await mockRepository.GetAllProductsAsync();
        }

        public async Task<int> GetProductsCountAsync()
        {
            return await mockRepository.GetProductsCountAsync();
        }
    }
}
