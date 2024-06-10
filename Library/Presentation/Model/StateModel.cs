using Presentation.Model.API;
using System;


namespace Presentation.Model
{
    internal class StateModel : IStateModel
    {
        private int _quantity;

        public StateModel(string guid, string productGuid, int quantity = 0)
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
        public string ProductGuid { get; set; }
        public double Price { get; set; }
    }
}
