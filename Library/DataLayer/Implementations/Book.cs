using DataLayer.API;
using System;

namespace DataLayer.Implementations
{
    public class Book : IBook
    {
        public Book(string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            Guid = System.Guid.NewGuid().ToString();
            Name = name;
            Price = price;
            Author = author;
            Publisher = publisher;
            Pages = pages;
            PublicationDate = publicationDate;
        }
        public string Guid { get; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Pages { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}