using DataLayer.API;

namespace DataLayer.Implementations
{
    public class Event : IEvent
    {
        public Event(IUser user, IState state, DateTime date, string guid)
        {
            User = user;
            State = state;
            Date = date;
            Guid = guid;
        }
        public IUser User { get; set; }
        public IState State { get; set; }
        public DateTime Date { get; set; }
        public string Guid { get; }
    }
}