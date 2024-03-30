namespace DataLayer.API
{
    public interface IEvent
    {
        IUser User { get; set; }
        IStatus Status { get; set; }
        DateTime Date { get; set; }
    }
}
