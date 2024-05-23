using DataLayer.API;

namespace DataLayer.Implementations
{
    public class State : IState
    {
        private int _quantity;

        public State(string guid, string productGuid, int quantity = 0)
        {
            Guid = string.IsNullOrEmpty(guid) ? System.Guid.NewGuid().ToString() : guid;
            ProductGuid = productGuid;
            Quantity = quantity;
        }

        public int Quantity 
        
        { 
            get => _quantity;
            set
            {
                _quantity = value;
            }
        }

        public string Guid { get; set; }
        public string ProductGuid{ get; set; }
        public double Price { get; set; }
    }
}