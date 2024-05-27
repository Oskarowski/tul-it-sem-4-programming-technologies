namespace Presentation.Model.API
{
    public interface IStateModel
    {
        string Guid { get; set; }
        string ProductGuid { get; set; }
        int Quantity { get; set; }
    }
}
