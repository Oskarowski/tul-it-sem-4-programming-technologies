using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTests.MockClasses
{
    internal class MockProductDTO : IProductDTO
    {
        public MockProductDTO(string guid, string name, double price,
                        string author, string publisher, int pages,
                        DateTime publicationDate)
        {
            Guid = guid;
            Name = name;
            Price = price;
            Author = author;
            Publisher = publisher;
            Pages = pages;
            PublicationDate = publicationDate;
        }

        public string Guid { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Pages { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
