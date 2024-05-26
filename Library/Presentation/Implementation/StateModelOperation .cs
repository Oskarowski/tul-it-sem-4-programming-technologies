using PresentationLayer.Model.API;
using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Implementation
{
    internal class StateModelOperation : IStateModelOperation
    {
        private IStateCRUD _stateCrud;

        public StateModelOperation(IStateCRUD? stateCrud = null)
        {
            this._stateCrud = stateCrud ?? IStateCRUD.CreateStateCRUD();
        }

        private IStateModel Map(IStateDTO state)
        {
            return new StateModel(state.Guid, state.ProductGuid, state.Quantity);
        }

        public async Task AddAsync(string guid, string productGuid, int quantity)
        {
            await this._stateCrud.AddStateAsync(guid, productGuid, quantity);
        }

        public async Task<IStateModel> GetAsync(string guid)
        {
            return this.Map(await this._stateCrud.GetStateAsync(guid));
        }

        public async Task UpdateAsync(string guid, string productGuid, int quantity)
        {
            await this._stateCrud.UpdateStateAsync(guid, productGuid, quantity);
        }

        public async Task DeleteAsync(string guid)
        {
            await this._stateCrud.DeleteStateAsync(guid);
        }

        public async Task<Dictionary<string, IStateModel>> GetAllAsync()
        {
            Dictionary<string, IStateModel> result = new Dictionary<string, IStateModel>();

            foreach (IStateDTO state in (await this._stateCrud.GetAllStatesAsync()).Values)
            {
                result.Add(state.Guid, this.Map(state));
            }

            return result;
        }

        public async Task<int> GetCountAsync()
        {
            return await this._stateCrud.GetStatesCountAsync();
        }
    }
}
