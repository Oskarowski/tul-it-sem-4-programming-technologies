using Service.API;
using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;

namespace Service.Implementation
{
    public class EventCRUD : IEventCRUD
    {
        private IDataRepository _dataRepository;

        public EventCRUD(IDataRepository dataRepository)
        {
            this._dataRepository = dataRepository;
        }

        public IEventDTO Map(IEvent @event)
        {
            return new EventDTO(@event.Guid, @event.StateGuid, @event.UserGuid,
                                    @event.CreatedAt, @event.Type);
        }

        public async Task AddEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            await _dataRepository.AddEventAsync(guid, stateGuid, userGuid, createdAt, type);
        }

        public async Task<IEventDTO> GetEventAsync(string guid)
        {
            return Map(await _dataRepository.GetEventAsync(guid));
        }

        public async Task UpdateEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            await _dataRepository.UpdateEventAsync(guid, stateGuid, userGuid, createdAt, type);
        }

        public async Task DeleteEventAsync(string guid)
        {
            await _dataRepository.DeleteEventAsync(guid);
        }

        public async Task<Dictionary<string, IEventDTO>> GetAllEventsAsync()
        {
            Dictionary<string, IEventDTO> result = new Dictionary<string, IEventDTO>();

            foreach (IEvent @event in (await _dataRepository.GetAllEventsAsync()).Values)
            {
                result.Add(@event.Guid, this.Map(@event));
            }

            return result;
        }

        public async Task<int> GetEventsCountAsync()
        {
            return await _dataRepository.GetEventsCountAsync();
        }
    }
}
