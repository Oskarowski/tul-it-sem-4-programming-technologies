namespace DataLayer.API
{
    public interface IUser
    {
        // guid, because is more commonly used in contexts of the Microsoft ecosystem
        string Guid { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        double Balance { get; set; }
        // stick to E.164 standard?
        string PhoneNumber { get; set; }
    }
}