namespace DataLayer.API
{
    public interface IEvent
    {
        IUser User { get; set; }
        IState State { get; set; }
        DateTime Date { get; set; }
        string Guid { get; }
    }
}
