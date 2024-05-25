using DataLayer.API;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IStateCRUD
    {
        static IStateCRUD CreateStateCRUD(IDataRepository? dataRepository = null)
        {
            return new StateCRUD(dataRepository ?? IDataRepository.CreateDatabase());
        }
        
        Task AddStateAsync(string guid, string productGuid, int quantity);
        
        Task<IStateDTO> GetStateAsync(string guid);
        
        Task UpdateStateAsync(string guid, string productGuid, int quantity);
        
        Task DeleteStateAsync(string guid);
        
        Task<Dictionary<string, IStateDTO>> GetAllStatesAsync();
        
        Task<int> GetStatesCountAsync();
    }
}
