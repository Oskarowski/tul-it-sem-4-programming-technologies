using DataLayer.API;
using DataLayer.Implementations;
using DataLayer.Implementations.Events;

namespace Tests
{
    public class PresetFiller : IDataFiller 
    {
        public void Fill(IDataContext context)
        {
            context.Users.Add(new User("John", "Doe", "johndoe@email", 100, 123456789, null));
            context.Users.Add(new User("Jane", "Doe", "janedoe@email", 200, 111111111, null));
            context.Users.Add(new User("Alice", "Smith", "alicesmith@email", 300, 121212121, null));
            context.Users.Add(new User("Bob", "Smith", "bobsmith@email", 400, 987654321, null));
            context.Users.Add(new User("Charlie", "Brown", "charliebrown@email", 500, 999999999, null));

            context.Products.Add(new Book("Old Man and the Sea", 10, "Ernest Hemingway", "Charles Sons", 300, new DateTime(1952, 1, 1)));
            context.Products.Add(new Book("The Great Gatsby", 20, "F. Scott Fitzgerald", "Charles Sons", 400, new DateTime(1925, 1, 1)));
            context.Products.Add(new Book("To Kill a Mockingbird", 30, "Harper Lee", "Charles Sons", 500, new DateTime(1960, 1, 1)));
            context.Products.Add(new Book("1984", 40, "George Orwell", "Charles Sons", 600, new DateTime(1949, 1, 1)));
            context.Products.Add(new Book("Brave New World", 50, "Aldous Huxley", "Charles Sons", 700, new DateTime(1932, 1, 1)));

            context.States.Add(new State(context.Products[0], 5));
            context.States.Add(new State(context.Products[1], 5));
            context.States.Add(new State(context.Products[2], 5));
            context.States.Add(new State(context.Products[3], 5));
            context.States.Add(new State(context.Products[4], 5));

            context.Events.Add(new Borrow(context.Users[0], context.States[0]));
            context.Events.Add(new Borrow(context.Users[1], context.States[1]));
            context.Events.Add(new Borrow(context.Users[3], context.States[3]));
            context.Events.Add(new Return(context.Users[0], context.States[0]));
            context.Events.Add(new Return(context.Users[1], context.States[1]));
            context.Events.Add(new Return(context.Users[3], context.States[3]));
            context.Events.Add(new Delivery(context.Users[4], context.States[4], 10));
        }
    }
}