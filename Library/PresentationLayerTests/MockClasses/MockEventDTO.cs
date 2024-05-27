using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTests.MockClasses
{
    internal class MockEventDTO : IEventDTO
    {
        public MockEventDTO(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
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
