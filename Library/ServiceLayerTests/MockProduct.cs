using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ServiceTests
{
    internal class MockProduct : IProduct
    {
        public MockProduct(string guid, string name, double price)
        {
            Guid = guid;
            Name = name;
            Price = price;
        }

        public string Guid { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
