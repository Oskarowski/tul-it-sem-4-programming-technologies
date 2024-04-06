using DataLayer.API;

namespace DataLayer.Implementations
{
    public class Return : IEvent
    {
        public Return(IUser user, IState state, DateTime date, string guid)
        {
            User = user;
            State = state;
            Date = date;
            Guid = guid;

            user.ProductsDic.Remove(state.Product.Guid);
            state.Quantity++;
        }
        public IUser User { get; set; }
        public IState State { get; set; }
        public DateTime Date { get; set; }
        public string Guid { get; }
    }
}