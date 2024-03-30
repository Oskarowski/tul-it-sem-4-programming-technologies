using DataLayer.API;

namespace DataLayer.Implementations
{
    public class Event : IEvent
    {
        public Event(IUser user, IStatus status, DateTime date)
        {
            User = user;
            Status = status;
            Date = date;
        }
        public IUser User { get; set; }
        public IStatus Status { get; set; }
        public DateTime Date { get; set; }
    }
}