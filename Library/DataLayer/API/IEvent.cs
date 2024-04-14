namespace DataLayer.API
{
    public interface IEvent
    {
        string Guid { get; }
        IUser User { get; }
        IState State { get; }
        DateTime CreatedAt { get; }
    }
}
