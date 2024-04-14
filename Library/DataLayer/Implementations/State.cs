using DataLayer.API;

namespace DataLayer.Implementations
{
    public class State : IState
    {
        private int _quantity;
        private DateTime _lastUpdatedDate;
        public State(IProduct product, int quantity = 0, string? guid = null)
        {
            Guid = string.IsNullOrEmpty(guid) ? System.Guid.NewGuid().ToString() : guid;
            Product = product;
            Quantity = quantity;
            _lastUpdatedDate = DateTime.Now;
        }
        public IProduct Product { get; set; }
        public int Quantity 
        { 
            get => _quantity;
            set
            {
                _quantity = value;
                _lastUpdatedDate = DateTime.Now;
            }
        }
        public DateTime LastUpdatedDate { get => _lastUpdatedDate; }
        public double Price { get; set; }
        public string Guid { get; }
    }
}