using DataLayer.API;
using System.Collections.Generic;

namespace DataLayer.Implementations
{
    public class User : IUser
    {
        public User(string firstName, string lastName, string email, double balance, int phoneNumber, Dictionary<string, IProduct> productsDic = null)
        {
            Guid = System.Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Balance = balance;
            PhoneNumber = phoneNumber;
            ProductsDic = productsDic ?? new Dictionary<string, IProduct>();
        }

        public string Guid { get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public double Balance { get; set; }

        public int PhoneNumber { get; set; }

        public Dictionary<string, IProduct> ProductsDic { get; set; }
    }
}