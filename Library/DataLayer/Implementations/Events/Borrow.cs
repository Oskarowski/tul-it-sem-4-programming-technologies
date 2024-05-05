using DataLayer.API;

namespace DataLayer.Implementations.Events
{
    public class Borrow : IEvent
    {
        private Borrow(IUser user, IState state, string? guid = null)
        {
            Guid = string.IsNullOrEmpty(guid) ? System.Guid.NewGuid().ToString() : guid;
            User = user;
            State = state;
            CreatedAt = DateTime.Now;

            // Check if the product is available
            if (state.Quantity <= 0)
            {
                throw new Exception("Product is not available");
            }

            // Should we allow borrowing the product if the user already has it?

            // Check if user has enough credits to borrow the product
            if (user.Balance < state.Product.Price)
            {
                throw new Exception("User balance is not enough to borrow the product");
            }

            user.ProductsDic.Add(state.Product.Guid, state.Product);
            state.Quantity--;
        }
        public static IEvent CreateBorrow(IUser user, IState state)
        {
            return new Borrow(user, state);
        }
        public string Guid { get; }
        public IUser User { get; }
        public IState State { get; }
        public DateTime CreatedAt { get; }
    }
}