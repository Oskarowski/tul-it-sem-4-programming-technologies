using DataLayer.API;

namespace DataLayer.Implementations
{
    public class Status : IStatus
    {
        public Status(IProduct product, int quantity, DateTime date, double price)
        {
            Product = product;
            Quantity = quantity;
            Date = date;
            Price = price;
        }
        public IProduct Product { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
    }
}