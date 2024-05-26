using PresentationLayer.Model.API;
using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Model.Implementation
{
    internal class StateModelOperation : IStateModelOperation
    {
        private IStateCRUD _stateCrud;

        public StateModelOperation(IStateCRUD? stateCrud = null)
        {
            _stateCrud = stateCrud ?? IStateCRUD.CreateStateCRUD();
        }

        private IStateModel Map(IStateDTO state)
        {
            return new StateModel(state.Guid, state.ProductGuid, state.Quantity);
        }

        public async Task AddAsync(string guid, string productGuid, int quantity)
        {
            await _stateCrud.AddStateAsync(guid, productGuid, quantity);
        }

        public async Task<IStateModel> GetAsync(string guid)
        {
            return Map(await _stateCrud.GetStateAsync(guid));
        }

        public async Task UpdateAsync(string guid, string productGuid, int quantity)
        {
            await _stateCrud.UpdateStateAsync(guid, productGuid, quantity);
        }

        public async Task DeleteAsync(string guid)
        {
            await _stateCrud.DeleteStateAsync(guid);
        }

        public async Task<Dictionary<string, IStateModel>> GetAllAsync()
        {
            Dictionary<string, IStateModel> result = new Dictionary<string, IStateModel>();

            foreach (IStateDTO state in (await _stateCrud.GetAllStatesAsync()).Values)
            {
                result.Add(state.Guid, Map(state));
            }

            return result;
        }

        public async Task<int> GetCountAsync()
        {
            return await _stateCrud.GetStatesCountAsync();
        }
    }
}
