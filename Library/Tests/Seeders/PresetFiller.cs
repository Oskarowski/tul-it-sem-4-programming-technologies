using DataLayer.API;
using DataLayer.Implementations;
using DataLayer.Implementations.Events;

namespace Tests.Seeders
{
    internal class PresetFiller : IDataFiller
    {
        private List<IUser> GeneratedUsers;
        private List<IProduct> GeneratedProducts;
        private List<IEvent> GeneratedEvents;
        private List<IState> GeneratedStates;

        public PresetFiller()
        {
            GeneratedUsers = new List<IUser>();
            GeneratedProducts = new List<IProduct>();
            GeneratedEvents = new List<IEvent>();
            GeneratedStates = new List<IState>();

            var user1 = new User("John", "Doe", "johndoe@email", 5000, 123456789, null);
            var user2 = new User("Jane", "Doe", "janedoe@email", 700, 111111111, null);
            var user3 = new User("Alice", "Smith", "alicesmith@email", 800, 121212121, null);
            var user4 = new User("Bob", "Smith", "bobsmith@email", 900, 987654321, null);
            var user5 = new User("Charlie", "Brown", "charliebrown@email", 1000, 999999999, null);

            GeneratedUsers.Add(user1);
            GeneratedUsers.Add(user2);
            GeneratedUsers.Add(user3);
            GeneratedUsers.Add(user4);
            GeneratedUsers.Add(user5);

            var product1 = new Book("Old Man and the Sea", 10, "Ernest Hemingway", "Charles Sons", 300, new DateTime(1952, 1, 1));
            var product2 = new Book("The Great Gatsby", 20, "F. Scott Fitzgerald", "Charles Sons", 400, new DateTime(1925, 1, 1));
            var product3 = new Book("To Kill a Mockingbird", 30, "Harper Lee", "Charles Sons", 500, new DateTime(1960, 1, 1));
            var product4 = new Book("1984", 40, "George Orwell", "Charles Sons", 600, new DateTime(1949, 1, 1));
            var product5 = new Book("Brave New World", 50, "Aldous Huxley", "Charles Sons", 700, new DateTime(1932, 1, 1));

            GeneratedProducts.Add(product1);
            GeneratedProducts.Add(product2);
            GeneratedProducts.Add(product3);
            GeneratedProducts.Add(product4);
            GeneratedProducts.Add(product5);

            var state1 = new State(product1, 50);
            var state2 = new State(product2, 40);
            var state3 = new State(product3, 30);
            var state4 = new State(product4, 20);
            var state5 = new State(product5, 15);

            GeneratedStates.Add(state1);
            GeneratedStates.Add(state2);
            GeneratedStates.Add(state3);
            GeneratedStates.Add(state4);
            GeneratedStates.Add(state5);

            var event1 = new Borrow(user1, state1);
            var event2 = new Borrow(user1, state2);
            var event3 = new Borrow(user1, state3);
            var event4 = new Borrow(user1, state4);
            var event5 = new Borrow(user1, state5);

            var event6 = new Borrow(user2, state2);

            GeneratedEvents.Add(event1);
            GeneratedEvents.Add(event2);
            GeneratedEvents.Add(event3);
            GeneratedEvents.Add(event4);
            GeneratedEvents.Add(event5);
            GeneratedEvents.Add(event6);

            var event7 = new Delivery(user3, state1, 25);
            var event8 = new Delivery(user4, state4, 40);
            var event9 = new Delivery(user5, state5, 40);

            GeneratedEvents.Add(event7);
            GeneratedEvents.Add(event8);
            GeneratedEvents.Add(event9);

            var event10 = new Return(user1, state1);
            var event11 = new Return(user2, state2);

            GeneratedEvents.Add(event10);
            GeneratedEvents.Add(event11);
        }

        public List<IUser> GetGeneratedUsers()
        {
            return GeneratedUsers;
        }
        public List<IProduct> GetGeneratedProducts()
        {
            return GeneratedProducts;
        }
        public List<IEvent> GetGeneratedEvents()
        {
            return GeneratedEvents;
        }
        public List<IState> GetGeneratedStates()
        {
            return GeneratedStates;
        }
    }
}