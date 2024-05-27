using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTests.MockClasses
{
    internal class MockStateCRUD : IStateCRUD
    {
        private MockRepository mockRepository = new MockRepository();

        public async Task AddStateAsync(string guid, string productGuid, int quantity)
        {
            await mockRepository.AddStateAsync(guid, productGuid, quantity);
        }

        public async Task<IStateDTO> GetStateAsync(string guid)
        {
            return await mockRepository.GetStateAsync(guid);
        }

        public async Task UpdateStateAsync(string guid, string productGuid, int quantity)
        {
            await mockRepository.UpdateStateAsync(guid, productGuid, quantity);
        }

        public async Task DeleteStateAsync(string guid)
        {
            await mockRepository.DeleteStateAsync(guid);
        }

        public async Task<Dictionary<string, IStateDTO>> GetAllStatesAsync()
        {
            return await mockRepository.GetAllStatesAsync();
        }

        public async Task<int> GetStatesCountAsync()
        {
            return await mockRepository.GetStatesCountAsync();
        }
    }
}
