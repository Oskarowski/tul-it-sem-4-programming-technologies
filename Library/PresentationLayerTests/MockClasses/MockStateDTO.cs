﻿using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTests.MockClasses
{
    internal class MockStateDTO : IStateDTO
    {
        public MockStateDTO(string guid, string productGuid, int quantity)
        {
            Guid = guid;
            ProductGuid = productGuid;
            Quantity = quantity;
        }

        public string Guid { get; set; }
        public string ProductGuid { get; set; }
        public int Quantity { get; set; }
    }
}
