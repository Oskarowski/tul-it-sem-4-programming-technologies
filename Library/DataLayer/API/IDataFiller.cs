using System.Collections.Generic;

namespace DataLayer.API
{
    public interface IDataFiller
    {
        List<IUser> GetGeneratedUsers();
        List<IProduct> GetGeneratedProducts();
        List<IEvent> GetGeneratedEvents();
        List<IState> GetGeneratedStates();
    }
}