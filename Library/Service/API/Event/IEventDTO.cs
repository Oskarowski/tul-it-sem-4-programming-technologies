using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IEventDTO
    {
        string Guid { get; set; }
        string StateGuid { get; set; }
        string UserGuid { get; set; }
        DateTime CreatedAt { get; set; }
        string Type { get; set; }
    }
}
