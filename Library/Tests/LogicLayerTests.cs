using DataLayer.Implementations;
using DataLayer.API;
using LogicLayer.Implementations;
using LogicLayer.API;

namespace Tests;

[TestClass]
public class LogicLayerTests
{
    [TestMethod]
    public void AddUserTest()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "Doe";
        const double balance = 100.0;
        const int phoneNumber = 1234567890;

        IUser user = new User(firstName, lastName, email, balance, phoneNumber, null);

        IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());
        dataRepository.AddUser(user);

        IDataService dataService = new DataService(dataRepository);

        Assert.AreEqual(user, dataService.GetUser(user.Guid));
        Assert.IsTrue(dataService.GetAllUsers().Contains(user));
    }

    [TestMethod]
    public void AddProductTest()
    {
        const string name = "Sample Book";
        const string author = "John Doe";
        const string publisher = "Publisher X";
        const double price = 29.99;
        const int pages = 299;
        DateTime publicationDate = new DateTime(2022, 1, 1);

        IProduct product = new Book(name, price, author, publisher, pages, publicationDate);

        IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());
        dataRepository.AddProduct(product);

        IDataService dataService = new DataService(dataRepository);

        Assert.AreEqual(product, dataService.GetProduct(product.Guid));
        Assert.IsTrue(dataService.GetAllProducts().Contains(product));
    }

    [TestMethod]
    public void AddStateTest()
    {
        const string name = "Sample Book";
        const string author = "John Doe";
        const string publisher = "Publisher X";
        const double price = 29.99;
        const int pages = 299;
        DateTime publicationDate = new DateTime(2022, 1, 1);
        IProduct product = new Book(name, price, author, publisher, pages, publicationDate);

        IState state = new State(product, 20, "1234567890");

        IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());
        dataRepository.AddState(state);

        IDataService dataService = new DataService(dataRepository);

        Assert.AreEqual(state, dataService.GetState(state.Guid));
        Assert.IsTrue(dataService.GetAllStates().Contains(state));
    }

    [TestMethod]
    public void AddEventTest()
    {
        // TODO Implement
        // const string firstName = "John";
        // const string lastName = "Doe";
        // const string email = "Doe";
        // const double balance = 100.0;
        // const int phoneNumber = 1234567890;
        // IUser user = new User(firstName, lastName, email, balance, phoneNumber, null);

        // const string name = "Sample Book";
        // const string author = "John Doe";
        // const string publisher = "Publisher X";
        // const double price = 29.99;
        // const int pages = 299;
        // DateTime publicationDate = new DateTime(2022, 1, 1);
        // IProduct product = new Book(name, price, author, publisher, pages, publicationDate);
        // IState state = new State(product, 20, "1234567890");

        // IEvent @event = new Event(user, state, DateTime.Now, "1111111111");

        // IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());
        // dataRepository.AddEvent(@event);

        // IDataService dataService = new DataService(dataRepository);

        // Assert.AreEqual(@event, dataService.GetEvent(@event.Guid));
        // Assert.IsTrue(dataService.GetAllEvents().Contains(@event));
    }

    // [TestMethod]
    // public void GetEventsByUserTest()
    // {
    //     const string firstName = "John";
    //     const string lastName = "Doe";
    //     const string email = "Doe";
    //     const double balance = 100.0;
    //     const int phoneNumber = 1234567890;
    //     IUser user = new User(firstName, lastName, email, balance, phoneNumber, null);

    //     const string name = "Sample Book";
    //     const string author = "John Doe";
    //     const string publisher = "Publisher X";
    //     const double price = 29.99;
    //     const int pages = 299;
    //     DateTime publicationDate = new DateTime(2022, 1, 1);
    //     IProduct product = new Book(name, price, author, publisher, pages, publicationDate);
    //     IState state = new State(product, 20, "1234567890");

    //     IEvent @event = new Event(user, state, DateTime.Now, "1111111111");

    //     IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());
    //     dataRepository.AddEvent(@event);

    //     IDataService dataService = new DataService(dataRepository);

    //     Assert.IsTrue(dataService.GetEventsByUser(user.Guid).Contains(@event));
    // }

    // [TestMethod]
    // public void GetEventsByProductTest()
    // {
    //     const string firstName = "John";
    //     const string lastName = "Doe";
    //     const string email = "Doe";
    //     const double balance = 100.0;
    //     const int phoneNumber = 1234567890;
    //     IUser user = new User(firstName, lastName, email, balance, phoneNumber, null);

    //     const string name = "Sample Book";
    //     const string author = "John Doe";
    //     const string publisher = "Publisher X";
    //     const double price = 29.99;
    //     const int pages = 299;
    //     DateTime publicationDate = new DateTime(2022, 1, 1);
    //     IProduct product = new Book(name, price, author, publisher, pages, publicationDate);
    //     IState state = new State(product, 20, "1234567890");

    //     IEvent @event = new Event(user, state, DateTime.Now, "1111111111");

    //     IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());
    //     dataRepository.AddEvent(@event);

    //     IDataService dataService = new DataService(dataRepository);

    //     Assert.IsTrue(dataService.GetEventsByProduct(product.Guid).Contains(@event));
    // }

    [TestMethod]
    public void GetProductByStateTest()
    {
        const string name = "Sample Book";
        const string author = "John Doe";
        const string publisher = "Publisher X";
        const double price = 29.99;
        const int pages = 299;
        DateTime publicationDate = new DateTime(2022, 1, 1);
        IProduct product = new Book(name, price, author, publisher, pages, publicationDate);
        IState state = new State(product, 20, "1234567890");

        IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());
        dataRepository.AddState(state);

        IDataService dataService = new DataService(dataRepository);

        Assert.AreEqual(product, dataService.GetProductByState(state.Guid));
    }

    // [TestMethod]
    // public void GetEventsByStateTest()
    // {
    //     const string firstName = "John";
    //     const string lastName = "Doe";
    //     const string email = "Doe";
    //     const double balance = 100.0;
    //     const int phoneNumber = 1234567890;
    //     IUser user = new User(firstName, lastName, email, balance, phoneNumber, null);

    //     const string name = "Sample Book";
    //     const string author = "John Doe";
    //     const string publisher = "Publisher X";
    //     const double price = 29.99;
    //     const int pages = 299;
    //     DateTime publicationDate = new DateTime(2022, 1, 1);
    //     IProduct product = new Book(name, price, author, publisher, pages, publicationDate);
    //     IState state = new State(product, 20, "1234567890");

    //     IEvent @event = new Event(user, state, DateTime.Now, "1111111111");

    //     IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());
    //     dataRepository.AddEvent(@event);

    //     IDataService dataService = new DataService(dataRepository);

    //     Assert.IsTrue(dataService.GetEventsByState(state.Guid).Contains(@event));
    // }

    [TestMethod]
    public void RemoveUserTest()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "Doe";
        const double balance = 100.0;
        const int phoneNumber = 1234567890;
        IUser user = new User(firstName, lastName, email, balance, phoneNumber, null);

        IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());
        dataRepository.AddUser(user);

        IDataService dataService = new DataService(dataRepository);

        dataService.RemoveUser(user.Guid);

        Assert.IsFalse(dataService.GetAllUsers().Contains(user));
    }

    [TestMethod]
    public void RemoveProductTest()
    {
        const string name = "Sample Book";
        const string author = "John Doe";
        const string publisher = "Publisher X";
        const double price = 29.99;
        const int pages = 299;
        DateTime publicationDate = new DateTime(2022, 1, 1);
        IProduct product = new Book(name, price, author, publisher, pages, publicationDate);

        IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());
        dataRepository.AddProduct(product);

        IDataService dataService = new DataService(dataRepository);

        dataService.RemoveProduct(product.Guid);

        Assert.IsFalse(dataService.GetAllProducts().Contains(product));
    }

    [TestMethod]
    public void RemoveStateTest()
    {
        const string name = "Sample Book";
        const string author = "John Doe";
        const string publisher = "Publisher X";
        const double price = 29.99;
        const int pages = 299;
        DateTime publicationDate = new DateTime(2022, 1, 1);
        IProduct product = new Book(name, price, author, publisher, pages, publicationDate);
        IState state = new State(product, 20, "1234567890");

        IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());
        dataRepository.AddState(state);

        IDataService dataService = new DataService(dataRepository);

        dataService.RemoveState(state.Guid);

        Assert.IsFalse(dataService.GetAllStates().Contains(state));
    }

    // [TestMethod]
    // public void RemoveEventTest()
    // {
    //     const string firstName = "John";
    //     const string lastName = "Doe";
    //     const string email = "Doe";
    //     const double balance = 100.0;
    //     const int phoneNumber = 1234567890;
    //     IUser user = new User(firstName, lastName, email, balance, phoneNumber, null);

    //     const string name = "Sample Book";
    //     const string author = "John Doe";
    //     const string publisher = "Publisher X";
    //     const double price = 29.99;
    //     const int pages = 299;
    //     DateTime publicationDate = new DateTime(2022, 1, 1);
    //     IProduct product = new Book(name, price, author, publisher, pages, publicationDate);
    //     IState state = new State(product, 20, "1234567890");

    //     IEvent @event = new Event(user, state, DateTime.Now, "1111111111");

    //     IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());
    //     dataRepository.AddEvent(@event);

    //     IDataService dataService = new DataService(dataRepository);

    //     dataService.RemoveEvent(@event.Guid);

    //     Assert.IsFalse(dataService.GetAllEvents().Contains(@event));
    // }
}