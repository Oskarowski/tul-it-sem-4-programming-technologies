using DataLayer.API;

namespace DataLayer.Implementations
{
    public class Event : IEvent
    {
        public Event(IUser user, IState state, DateTime date)
        {
            User = user;
            State = state;
            Date = date;
        }
        public IUser User { get; set; }
        public IState State { get; set; }
        public DateTime Date { get; set; }
    }
}