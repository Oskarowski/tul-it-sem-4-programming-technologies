using Service.API;
using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class StateCRUD : IStateCRUD
    {
        private IDataRepository _dataRepository;

        public StateCRUD(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IStateDTO Map(IState state)
        {
            return new StateDTO(state.Guid, state.ProductGuid, state.Quantity);
        }

        public async Task AddStateAsync(string guid, string productGuid, int quantity)
        {
            await _dataRepository.AddStateAsync(guid, productGuid, quantity);
        }

        public async Task<IStateDTO> GetStateAsync(string guid)
        {
            return Map(await _dataRepository.GetStateAsync(guid));
        }

        public async Task UpdateStateAsync(string guid, string productGuid, int quantity)
        {
            await _dataRepository.UpdateStateAsync(guid, productGuid, quantity);
        }

        public async Task DeleteStateAsync(string guid)
        {
            await _dataRepository.DeleteStateAsync(guid);
        }

        public async Task<Dictionary<string, IStateDTO>> GetAllStatesAsync()
        {
            Dictionary<string, IStateDTO> result = new Dictionary<string, IStateDTO>();

            foreach (IState state in (await _dataRepository.GetAllStatesAsync()).Values)
            {
                result.Add(state.Guid, this.Map(state));
            }

            return result;
        }

        public async Task<int> GetStatesCountAsync()
        {
            return (await _dataRepository.GetAllStatesAsync()).Count;
        }
    }
}
