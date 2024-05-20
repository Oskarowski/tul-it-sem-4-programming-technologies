using System.Collections.Generic;

namespace DataLayer.API
{
    public interface IDataContext
    {
        List<IUser> Users { get; set; }

        List<IProduct> Products { get; set; }

        List<IEvent> Events { get; set; }

        List<IState> States { get; set; }
    }
}