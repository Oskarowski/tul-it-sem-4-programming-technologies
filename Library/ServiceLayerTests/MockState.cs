using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ServiceTests
{
    internal class MockState : IState
    {
        public MockState(string guid, int quantity, string productGuid)
        {
            Guid = guid;
            Quantity = quantity;
            ProductGuid = productGuid;
        }

        public string Guid { get; set; }
        public int Quantity { get; set; }
        public string ProductGuid { get; set; }
    }
}
