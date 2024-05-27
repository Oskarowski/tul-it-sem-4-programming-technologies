using System;
using Presentation.Model.API;
using Service.API;


namespace Presentation.Model.Implementation
{
    internal class EventModelOperation : IEventModelOperation
    {
        private IEventCRUD _eventCRUD;

        public EventModelOperation(IEventCRUD? eventCrud = null)
        {
            _eventCRUD = eventCrud ?? IEventCRUD.CreateEventCRUD();
        }

        private IEventModel Map(IEventDTO even)
        {
            return new EventModel(even.Guid, even.StateGuid, even.UserGuid, even.CreatedAt, even.Type);
        }

        public async Task AddAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            await _eventCRUD.AddEventAsync(guid, stateGuid, userGuid, createdAt, type);
        }

        public async Task<IEventModel> GetAsync(string guid, string type)
        {
            return Map(await _eventCRUD.GetEventAsync(guid));
        }

        public async Task UpdateAsync(string guid, string stateGuid, string userIGuid, DateTime createdAt, string type)
        {
            await _eventCRUD.UpdateEventAsync(guid, stateGuid, userIGuid, createdAt, type);
        }

        public async Task DeleteAsync(string guid)
        {
            await _eventCRUD.DeleteEventAsync(guid);
        }

        public async Task<Dictionary<string, IEventModel>> GetAllAsync()
        {
            Dictionary<string, IEventModel> result = new Dictionary<string, IEventModel>();

            foreach (IEventDTO even in (await _eventCRUD.GetAllEventsAsync()).Values)
            {
                result.Add(even.Guid, Map(even));
            }

            return result;
        }

        public async Task<int> GetCountAsync()
        {
            return await _eventCRUD.GetEventsCountAsync();
        }
    }
}
