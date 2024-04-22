using DataLayer.API;
using DataLayer.Implementations;
using DataLayer.Implementations.Events;

namespace Tests
{
    public class PresetFiller : IDataFiller 
    {
        public void Fill(IDataContext context)
        {
            var user1 = new User("John", "Doe", "johndoe@email", 5000, 123456789, null);
            var user2 = new User("Jane", "Doe", "janedoe@email", 700, 111111111, null);
            var user3 = new User("Alice", "Smith", "alicesmith@email", 800, 121212121, null);
            var user4 = new User("Bob", "Smith", "bobsmith@email", 900, 987654321, null);
            var user5 = new User("Charlie", "Brown", "charliebrown@email", 1000, 999999999, null);
            
            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.Users.Add(user4);
            context.Users.Add(user5);

            var product1 = new Book("Old Man and the Sea", 10, "Ernest Hemingway", "Charles Sons", 300, new DateTime(1952, 1, 1));
            var product2 = new Book("The Great Gatsby", 20, "F. Scott Fitzgerald", "Charles Sons", 400, new DateTime(1925, 1, 1));
            var product3 = new Book("To Kill a Mockingbird", 30, "Harper Lee", "Charles Sons", 500, new DateTime(1960, 1, 1));
            var product4 = new Book("1984", 40, "George Orwell", "Charles Sons", 600, new DateTime(1949, 1, 1));
            var product5 = new Book("Brave New World", 50, "Aldous Huxley", "Charles Sons", 700, new DateTime(1932, 1, 1));

            context.Products.Add(product1);
            context.Products.Add(product2);
            context.Products.Add(product3);
            context.Products.Add(product4);
            context.Products.Add(product5);

            var state1 = new State(product1, 50);
            var state2 = new State(product2, 40);
            var state3 = new State(product3, 30);
            var state4 = new State(product4, 20);
            var state5 = new State(product5, 15);

            context.States.Add(state1);
            context.States.Add(state2);
            context.States.Add(state3);
            context.States.Add(state4);
            context.States.Add(state5);

            var event1 = new Borrow(user1, state1);
            var event2 = new Borrow(user1, state2);
            var event3 = new Borrow(user1, state3);
            var event4 = new Borrow(user1, state4);
            var event5 = new Borrow(user1, state5);

            var event6 = new Borrow(user2, state2);

            context.Events.Add(event1);
            context.Events.Add(event2);
            context.Events.Add(event3);
            context.Events.Add(event4);
            context.Events.Add(event5);
            context.Events.Add(event6);

            var event7 = new Delivery(user3, state1, 25);
            var event8 = new Delivery(user4, state4, 40);
            var event9 = new Delivery(user5, state5, 40);

            context.Events.Add(event7);
            context.Events.Add(event8);
            context.Events.Add(event9);

            var event10 = new Return(user1, state1);
            var event11 = new Return(user2, state2);
            
            context.Events.Add(event10);
            context.Events.Add(event11);
        }
    }
}