using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IStateDTO
    {
        string Guid { get; set; }
        string ProductGuid { get; set; }
        int Quantity { get; set; }
    }
}
