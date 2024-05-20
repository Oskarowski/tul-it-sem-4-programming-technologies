using System;

namespace DataLayer.API
{
    public interface IBook : IProduct
    {
        string Author { get; set; }
        string Publisher { get; set; }
        int Pages { get; set; }
        DateTime PublicationDate { get; set; }
    }
}