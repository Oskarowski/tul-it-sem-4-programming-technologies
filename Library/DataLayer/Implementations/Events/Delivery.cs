using DataLayer.API;

namespace DataLayer.Implementations.Events
{
    public class Delivery : IEvent
    {
        public Delivery(IUser user, IState state, int amount, string? guid = null)
        {
            Guid = string.IsNullOrEmpty(guid) ? System.Guid.NewGuid().ToString() : guid;
            User = user;
            State = state;
            CreatedAt = DateTime.Now;

            if(amount <= 0)
            {
                throw new Exception("Amount must be greater than 0");
            }

            state.Quantity += amount;
        }

        public string Guid { get; }
        public IUser User { get; }
        public IState State { get; }
        public DateTime CreatedAt { get; }
    }
}