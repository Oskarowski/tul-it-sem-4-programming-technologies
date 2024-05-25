using DataLayer.API;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IEventCRUD
    {
        static IEventCRUD CreateEventCRUD(IDataRepository? dataRepository = null)
        {
            return new EventCRUD(dataRepository ?? IDataRepository.CreateDatabase());
        }

        Task AddEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type);

        Task<IEventDTO> GetEventAsync(string guid);

        Task UpdateEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type);

        Task DeleteEventAsync(string guid);

        Task<Dictionary<string, IEventDTO>> GetAllEventsAsync();

        Task<int> GetEventsCountAsync();
    }
}
