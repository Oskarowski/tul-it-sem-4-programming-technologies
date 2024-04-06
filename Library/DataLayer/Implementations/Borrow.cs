using DataLayer.API;

namespace DataLayer.Implementations
{
    public class Borrow : IEvent
    {
        public Borrow(IUser user, IState state, DateTime date, string guid)
        {
            User = user;
            State = state;
            Date = date;
            Guid = guid;

            user.ProductsDic.Add(state.Product.Guid, state.Product);
            state.Quantity--;
        }
        public IUser User { get; set; }
        public IState State { get; set; }
        public DateTime Date { get; set; }
        public string Guid { get; }
    }
}