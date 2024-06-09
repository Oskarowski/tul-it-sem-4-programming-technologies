using Presentation.Model.API;
using System;


namespace Presentation.Model
{
    internal class EventModel : IEventModel
    {
        public EventModel(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            Guid = guid;
            StateGuid = stateGuid;
            UserGuid = userGuid;
            CreatedAt = createdAt;
            Type = type;
        }

        public string Guid { get; set; }

        public string StateGuid { get; set; }

        public string UserGuid { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Type { get; set; }
    }
}
