using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ServiceTests
{
    internal class MockEvent : IEvent
    {
        public MockEvent(string guid, string userGuid, string stateGuid, string type, DateTime createdAt)
        {
            Guid = guid;
            UserGuid = userGuid;
            StateGuid = stateGuid;
            Type = type;
            CreatedAt = createdAt;
        }

        public string Guid { get; set; }

        public string UserGuid { get; set; }

        public string StateGuid { get; set; }

        public string Type { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
