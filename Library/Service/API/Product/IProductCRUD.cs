using DataLayer.API;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IProductCRUD
    {
        static IProductCRUD CreateBookCRUD(IDataRepository? dataRepository = null)
        {
            return new ProductCRUD(dataRepository ?? IDataRepository.CreateDatabase());
        }

        Task AddProductAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate);
        
        Task<IProductDTO> GetProductAsync(string guid);

        Task UpdateProductAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate);
        
        Task DeleteProductAsync(string guid);
        
        Task<Dictionary<string, IProductDTO>> GetAllProductsAsync();
        
        Task<int> GetProductsCountAsync();
    }
}
