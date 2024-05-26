using PresentationLayer.Model.API;
using System;


namespace PresentationLayer.Implementation
{
    internal class EventModel : IEventModel
    {
        public EventModel(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            this.Guid = guid;
            this.StateGuid = stateGuid;
            this.UserGuid = userGuid;
            this.CreatedAt = createdAt;
            this.Type = type;
        }

        public string Guid { get; set; }

        public string StateGuid { get; set; }

        public string UserGuid { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Type { get; set; }
    }
}
