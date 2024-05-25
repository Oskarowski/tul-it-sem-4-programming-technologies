using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IUserDTO
    {
        string Guid { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        double Balance { get; set; }
        string PhoneNumber { get; set; }
    }
}
