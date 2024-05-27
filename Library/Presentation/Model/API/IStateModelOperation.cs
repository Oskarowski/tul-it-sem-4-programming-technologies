using Presentation.Model.Implementation;
using Service.API;

namespace Presentation.Model.API
{
    public interface IStateModelOperation
    {
        static IStateModelOperation CreateModelOperation(IStateCRUD? stateCrud = null)
        {
            return new StateModelOperation(stateCrud ?? IStateCRUD.CreateStateCRUD());
        }

        Task AddAsync(string guid, string productGuid, int quantity);

        Task<IStateModel> GetAsync(string guid);

        Task UpdateAsync(string guid, string productGuid, int quantity);

        Task DeleteAsync(string guid);

        Task<Dictionary<string, IStateModel>> GetAllAsync();

        Task<int> GetCountAsync();
    }
}
