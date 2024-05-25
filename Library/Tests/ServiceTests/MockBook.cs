using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ServiceTests
{
    internal class MockBook : IBook
    {
        public MockBook(string guid, string name, string author, double price, string publisher, int pages, DateTime publicationDate)
        {
            Guid = guid;
            Name = name;
            Author = author;
            Price = price;
            Publisher = publisher;
            Pages = pages;
            PublicationDate = publicationDate;
        }

        public string Guid { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string Publisher { get; set; }
        public int Pages { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
