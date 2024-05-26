using System;
using PresentationLayer.Model.API;
using Service.API;


namespace PresentationLayer.Implementation
{
    internal class EventModelOperation : IEventModelOperation
    {
        private IEventCRUD _eventCRUD;

        public EventModelOperation(IEventCRUD? eventCrud = null)
        {
            this._eventCRUD = eventCrud ?? IEventCRUD.CreateEventCRUD();
        }

        private IEventModel Map(IEventDTO even)
        {
            return new EventModel(even.Guid, even.StateGuid, even.UserGuid, even.CreatedAt, even.Type);
        }

        public async Task AddAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            await this._eventCRUD.AddEventAsync(guid, stateGuid, userGuid, createdAt, type);
        }

        public async Task<IEventModel> GetAsync(string guid, string type)
        {
            return this.Map(await this._eventCRUD.GetEventAsync(guid));
        }

        public async Task UpdateAsync(string guid, string stateGuid, string userIGuid, DateTime createdAt, string type)
        {
            await this._eventCRUD.UpdateEventAsync(guid, stateGuid, userIGuid, createdAt, type);
        }

        public async Task DeleteAsync(string guid)
        {
            await this._eventCRUD.DeleteEventAsync(guid);
        }

        public async Task<Dictionary<string, IEventModel>> GetAllAsync()
        {
            Dictionary<string, IEventModel> result = new Dictionary<string, IEventModel>();

            foreach (IEventDTO even in (await this._eventCRUD.GetAllEventsAsync()).Values)
            {
                result.Add(even.Guid, this.Map(even));
            }

            return result;
        }

        public async Task<int> GetCountAsync()
        {
            return await this._eventCRUD.GetEventsCountAsync();
        }
    }
}
