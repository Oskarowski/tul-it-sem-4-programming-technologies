using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IProductDTO
    {
        string Guid { get; set; }
        string Name { get; set; }
        double Price { get; set; }
        string Author { get; set; }
        string Publisher { get; set; }
        int Pages { get; set; }
        DateTime PublicationDate { get; set; }
    }
}
