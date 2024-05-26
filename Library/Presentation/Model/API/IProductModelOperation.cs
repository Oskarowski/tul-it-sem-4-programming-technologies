using Presentation.Model.Implementation;
using Service.API;

namespace Presentation.Model.API
{
    public interface IProductModelOperation
    {
        static IProductModelOperation CreateModelOperation(IProductCRUD? productCrud = null)
        {
            return new ProductModelOperation(productCrud ?? IProductCRUD.CreateBookCRUD());
        }

        Task AddAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate);

        Task<IProductModel> GetAsync(string guid);

        Task UpdateAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate);

        Task DeleteAsync(string guid);

        Task<Dictionary<string, IProductModel>> GetAllAsync();

        Task<int> GetCountAsync();
    }
}
