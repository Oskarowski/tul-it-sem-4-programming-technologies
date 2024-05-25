using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class UserDTO : IUserDTO
    {
        public UserDTO(string guid, string firstName, string lastName,
                            string email, double balance, string phoneNumber)
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
