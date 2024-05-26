using Service.API;

namespace PresentationLayer.Model.API
{
    public interface IEventModelOperation
    {
        static IEventModelOperation CreateModelOperation(IEventCRUD? eventCrud = null)
        {
            return new EventModelOperation(eventCrud ?? IEventCRUD.CreateEventCRUD());
        }

        Task AddAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type);

        Task<IEventModel> GetAsync(string guid, string type);

        Task UpdateAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type);

        Task DeleteAsync(string guid);

        Task<Dictionary<string, IEventModel>> GetAllAsync();

        Task<int> GetCountAsync();
    }
}
