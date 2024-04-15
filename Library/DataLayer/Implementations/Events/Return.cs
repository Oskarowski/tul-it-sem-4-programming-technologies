using DataLayer.API;

namespace DataLayer.Implementations.Events
{
    public class Return : IEvent
    {
        public Return(IUser user, IState state, string? guid = null)
        {
            Guid = string.IsNullOrEmpty(guid) ? System.Guid.NewGuid().ToString() : guid;
            User = user;
            State = state;
            CreatedAt = DateTime.Now;

            // Check if the product is in the user's inventory because we can't return a product that we don't have
            if (!user.ProductsDic.ContainsKey(state.Product.Guid))
            {
                throw new Exception("Product not found in user's inventory");
            }

            user.ProductsDic.Remove(state.Product.Guid);
            state.Quantity++;
        }
        public string Guid { get; }
        public IUser User { get; }
        public IState State { get; }
        public DateTime CreatedAt { get; }
    }
}