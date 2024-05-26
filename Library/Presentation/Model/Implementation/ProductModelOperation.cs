using System;
using PresentationLayer.Model.API;
using Service.API;

namespace PresentationLayer.Model.Implementation
{
    internal class ProductModelOperation : IProductModelOperation
    {
        private IProductCRUD _productCRUD;

        public ProductModelOperation(IProductCRUD? productCrud = null)
        {
            _productCRUD = productCrud ?? IProductCRUD.CreateBookCRUD();
        }

        private IProductModel Map(IProductDTO product)
        {
            return new ProductModel(product.Guid, product.Name, product.Price, product.Author, product.Publisher, product.Pages, product.PublicationDate);
        }

        public async Task AddAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            await _productCRUD.AddProductAsync(guid, name, price, author, publisher, pages, publicationDate);
        }

        public async Task<IProductModel> GetAsync(string guid)
        {
            return Map(await _productCRUD.GetProductAsync(guid));
        }

        public async Task UpdateAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            await _productCRUD.UpdateProductAsync(guid, name, price, author, publisher, pages, publicationDate);
        }

        public async Task DeleteAsync(string guid)
        {
            await _productCRUD.DeleteProductAsync(guid);
        }

        public async Task<Dictionary<string, IProductModel>> GetAllAsync()
        {
            Dictionary<string, IProductModel> result = new Dictionary<string, IProductModel>();

            foreach (IProductDTO product in (await _productCRUD.GetAllProductsAsync()).Values)
            {
                result.Add(product.Guid, Map(product));
            }

            return result;
        }

        public async Task<int> GetCountAsync()
        {
            return await _productCRUD.GetProductsCountAsync();
        }
    }
}
