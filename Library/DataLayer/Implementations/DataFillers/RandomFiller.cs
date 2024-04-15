using System;
using DataLayer.API;
using DataLayer.Implementations.Events;

namespace DataLayer.Implementations
{
    public class RandomFiller : IDataFiller
    {
        private Random random = new Random();
        public void Fill(IDataContext context)
        {
            int usersCount = random.Next(5, 11);
            for (int i = 0; i < usersCount; i++)
            {
                context.Users.Add(new User(
                    GetRandomFirstName(),
                    GetRandomLastName(),
                    $"{GetRandomFirstName().ToLower()}{GetRandomLastName().ToLower()}@email.com",
                    random.Next(100, 1000),
                    GetRandomPhoneNumber(),
                    null
                ));
            }

            int productsCount = random.Next(5, 11);
            for (int i = 0; i < productsCount; i++)
            {
                context.Products.Add(new Book(
                    GetRandomTitle(),
                    random.Next(10, 100),
                    GetRandomAuthor(),
                    GetRandomPublisher(),
                    random.Next(50, 1000),
                    GetRandomDate()
                ));
            }

            int statesCount = context.Products.Count;
            for (int i = 0; i < statesCount; i++)
            {
                context.States.Add(new State(
                    context.Products[i],
                    random.Next(1, 10)
                ));
            }
            
            int eventsCount = random.Next(5, 11);
            int borrowsCount = 0;
            List<IUser> usersBorrowing = new List<IUser>();
            List<IState> statesBorrowed = new List<IState>();
            for (int i = 0; i < eventsCount; i++)
            {
                int userIndex = random.Next(context.Users.Count);
                int stateIndex = random.Next(context.States.Count);
                int randomEvent = random.Next(3);
                if (randomEvent == 0)
                {
                    context.Events.Add(new Borrow(context.Users[userIndex], context.States[stateIndex]));
                    borrowsCount++;
                    usersBorrowing.Add(context.Users[userIndex]);
                    statesBorrowed.Add(context.States[stateIndex]);
                }
                else if (randomEvent == 1 && borrowsCount > 0)
                {
                    context.Events.Add(new Return(usersBorrowing[borrowsCount-1], statesBorrowed[borrowsCount-1]));
                    borrowsCount--;
                    usersBorrowing.RemoveAt(borrowsCount);
                    statesBorrowed.RemoveAt(borrowsCount);
                }
                else if (randomEvent == 1 && borrowsCount == 0)
                {
                    i--;
                }
                else if (randomEvent == 2)
                {
                    context.Events.Add(new Delivery(context.Users[userIndex], context.States[stateIndex], random.Next(1, 10)));
                }
            }
        }
        private string GetRandomFirstName()
        {
            string[] firstNames = {
                "John", "Jane", "Alice", "Bob", "Charlie",
                "Michael", "Emily", "David", "Sarah", "Daniel",
                "Matthew", "Jessica", "Andrew", "Jennifer", "James",
                "Linda", "William", "Karen", "Joseph", "Maria",
                "Robert", "Susan", "Christopher", "Nancy", "Daniel",
                "Karen", "Thomas", "Lisa", "Brian", "Margaret",
                "Steven", "Betty", "Timothy", "Dorothy", "Kevin",
                "Sandra", "Richard", "Ashley", "Mark", "Kimberly",
                "Jeffrey", "Donna", "Scott", "Emily", "Charles",
                "Carol", "Paul", "Michelle", "Anthony", "Laura"
            };
            return firstNames[random.Next(firstNames.Length)];
        }
        private string GetRandomLastName()
        {
            string[] lastNames = {
                "Smith", "Johnson", "Williams", "Brown", "Jones",
                "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
                "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson",
                "Thomas", "Taylor", "Moore", "Jackson", "Martin",
                "Lee", "Perez", "Thompson", "White", "Harris",
                "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
                "Walker", "Young", "Allen", "King", "Wright",
                "Scott", "Torres", "Nguyen", "Hill", "Flores",
                "Green", "Adams", "Nelson", "Baker", "Hall",
                "Rivera", "Campbell", "Mitchell", "Carter", "Roberts"
            };
            return lastNames[random.Next(lastNames.Length)];
        }
        private int GetRandomPhoneNumber()
        {
            return random.Next(100000000, 1000000000); // 9-digit random number
        }
        private string GetRandomTitle()
        {
            string[] titles = {
                "The Catcher in the Rye", "To Kill a Mockingbird", "1984", "The Great Gatsby", "Pride and Prejudice",
                "Harry Potter and the Philosopher's Stone", "The Hobbit", "The Lord of the Rings", "The Hunger Games", "The Da Vinci Code",
                "The Chronicles of Narnia", "Twilight", "The Alchemist", "The Fault in Our Stars", "The Kite Runner",
                "A Song of Ice and Fire", "The Hitchhiker's Guide to the Galaxy", "The Shining", "The Girl with the Dragon Tattoo", "Gone Girl",
                "Brave New World", "Animal Farm", "The Adventures of Huckleberry Finn", "Moby-Dick", "Frankenstein",
                "Dracula", "Alice's Adventures in Wonderland", "The War of the Worlds", "The Picture of Dorian Gray", "The Bell Jar"
            };
            return titles[random.Next(titles.Length)];
        }
        private string GetRandomAuthor()
        {
            string[] authors = {
                "J.D. Salinger", "Harper Lee", "George Orwell", "F. Scott Fitzgerald", "Jane Austen",
                "J.K. Rowling", "J.R.R. Tolkien", "George R.R. Martin", "Suzanne Collins", "Dan Brown",
                "C.S. Lewis", "Stephenie Meyer", "Paulo Coelho", "John Green", "Khaled Hosseini",
                "George R.R. Martin", "Douglas Adams", "Stephen King", "Stieg Larsson", "Gillian Flynn",
                "Aldous Huxley", "George Orwell", "Mark Twain", "Herman Melville", "Mary Shelley",
                "Bram Stoker", "Lewis Carroll", "H.G. Wells", "Oscar Wilde", "Sylvia Plath"
            };
            return authors[random.Next(authors.Length)];
        }
        private string GetRandomPublisher()
        {
            string[] publishers = {
                "Random House", "Penguin Books", "HarperCollins", "Simon & Schuster", "Hachette Livre",
                "Macmillan Publishers", "Scholastic Corporation", "Pearson Education", "Bloomsbury Publishing", "Oxford University Press",
                "Cambridge University Press", "Wiley", "Springer Nature", "McGraw-Hill Education", "Cengage",
                "Houghton Mifflin Harcourt", "Elsevier", "Taylor & Francis", "Harvard University Press", "MIT Press"
            };
            return publishers[random.Next(publishers.Length)];
        }
        private DateTime GetRandomDate()
        {
            // Random date between 1900 and now
            DateTime start = new DateTime(1900, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}