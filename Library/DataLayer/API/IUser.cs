using System.Collections.Generic;

namespace DataLayer.API
{
    public interface IUser
    {
        // guid, because is more commonly used in contexts of the Microsoft ecosystem
        string Guid { get; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        double Balance { get; set; }
        // stick to E.164 standard?
        int PhoneNumber { get; set; }
        // string Role { get; set; }
        Dictionary<string, IProduct> ProductsDic { get; set; }
    }
}