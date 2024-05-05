using DataLayer.API;

namespace DataLayer.Implementations
{
    public class User : IUser
    {
        private User(string firstName, string lastName, string email, double balance, int phoneNumber, Dictionary<string, IProduct>? productsDic)
        {
            Guid = System.Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Balance = balance;
            PhoneNumber = phoneNumber;
            ProductsDic = productsDic ?? new Dictionary<string, IProduct>();
        }
        public static IUser CreateUser(string firstName, string lastName, string email, double balance, int phoneNumber, Dictionary<string, IProduct>? productsDic)
        {
            return new User(firstName, lastName, email, balance, phoneNumber, productsDic);
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