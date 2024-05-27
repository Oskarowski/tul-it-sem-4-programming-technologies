namespace DataLayer.API
{
    public interface IEvent
    {
        string Guid { get; set; }
        string UserGuid { get; set; }
        string StateGuid { get; set; }
        string Type { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
