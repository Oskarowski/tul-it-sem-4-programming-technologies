using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ServiceTests
{
    internal class MockUser : IUser
    {
        public MockUser(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            Guid = guid;
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
