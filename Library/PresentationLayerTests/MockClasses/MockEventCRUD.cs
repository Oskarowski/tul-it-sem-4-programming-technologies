using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTests.MockClasses
{
    internal class MockEventCRUD : IEventCRUD
    {
        private MockRepository mockRepository = new MockRepository();

        public async Task AddEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            mockRepository.Events.Add(guid, new MockEventDTO(guid, stateGuid, userGuid, createdAt, type));
        }

        public async Task<IEventDTO> GetEventAsync(string guid)
        {
            return await mockRepository.GetEventAsync(guid);
        }

        public async Task UpdateEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            await mockRepository.UpdateEventAsync(guid, stateGuid, userGuid, createdAt, type);
        }

        public async Task DeleteEventAsync(string guid)
        {
            await mockRepository.DeleteEventAsync(guid);
        }

        public async Task<Dictionary<string, IEventDTO>> GetAllEventsAsync()
        {
            return await mockRepository.GetAllEventsAsync();
        }

        public async Task<int> GetEventsCountAsync()
        {
            return await mockRepository.GetEventsCountAsync();
        }
    }
}
