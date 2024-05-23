namespace DataLayer.API
{
    public interface IState
    {
        string Guid { get; set; }
        int Quantity { get; set; }
        string ProductGuid { get; set; }
    }
}
