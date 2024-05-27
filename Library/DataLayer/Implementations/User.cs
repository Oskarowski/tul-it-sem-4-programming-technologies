using DataLayer.API;

namespace DataLayer.Implementations
{
    public class User : IUser
    {
        public User(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            Guid = string.IsNullOrEmpty(guid) ? System.Guid.NewGuid().ToString() : guid;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Balance = balance;
            PhoneNumber = phoneNumber;
        }

        public string Guid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public double Balance { get; set; }

        public string PhoneNumber { get; set; }
    }
}