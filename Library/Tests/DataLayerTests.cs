using DataLayer.Implementations;
using DataLayer.Implementations.Events;
using DataLayer.API;

namespace Tests;

[TestClass]
public class DataLayerTests
{
    [TestMethod]
    public void UserTests()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "Doe";
        const double balance = 100.0;
        const int phoneNumber = 1234567890;

        IUser user = new User(firstName, lastName, email, balance, phoneNumber, null);

        Assert.AreEqual(firstName, user.FirstName);
        Assert.AreEqual(lastName, user.LastName);
        Assert.AreEqual(email, user.Email);
        Assert.AreEqual(balance, user.Balance);
        Assert.AreEqual(phoneNumber, user.PhoneNumber);
        Assert.IsNotNull(user.Guid);

        Book book1 = new Book("Book1", 10.0, "Author1", "Publisher1", 100, new DateTime(2022, 1, 1));
        Book book2 = new Book("Book2", 20.0, "Author2", "Publisher2", 200, new DateTime(2022, 2, 2));

        Dictionary<string, IProduct> BooksCollection = new Dictionary<string, IProduct>
        {
            { book1.Guid, book1 },
            { book2.Guid, book2 }
        };

        user.ProductsDic = BooksCollection;

        Assert.AreEqual(book1, user.ProductsDic[book1.Guid]);
        Assert.AreEqual(book2, user.ProductsDic[book2.Guid]);

        IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());

        dataRepository.AddUser(user);

        Assert.IsTrue(dataRepository.GetAllUsers().Contains(user));

        Assert.ThrowsException<Exception>(() => dataRepository.GetUser("For sure not even a valid guid"));

        Assert.AreEqual(user, dataRepository.GetUser(user.Guid));

        Assert.IsTrue(dataRepository.DoesUserExist(user.Guid));
        Assert.IsFalse(dataRepository.DoesUserExist("For sure not even a valid guid"));

        user.FirstName = "New Name is Jane";

        dataRepository.UpdateUser(user);
        user = dataRepository.GetUser(user.Guid);

        Assert.AreEqual("New Name is Jane", user.FirstName);

        dataRepository.RemoveUser(user.Guid);
        Assert.AreEqual(0, dataRepository.GetAllUsers().Count);

    }

    [TestMethod]
    public void BookTests()
    {
        const string name = "Sample Book";
        const string author = "John Doe";
        const string publisher = "Publisher X";
        const double price = 29.99;
        const int pages = 299;
        DateTime publicationDate = new DateTime(2022, 1, 1);

        IBook book = new Book(name, price, author, publisher, pages, publicationDate);

        Assert.AreEqual(name, book.Name);
        Assert.AreEqual(author, book.Author);
        Assert.AreEqual(publisher, book.Publisher);
        Assert.AreEqual(price, book.Price);
        Assert.AreEqual(pages, book.Pages);
        Assert.AreEqual(publicationDate, book.PublicationDate);


        Book book1 = new Book("Book1", 10.0, "Author1", "Publisher1", 100, new DateTime(2022, 1, 1));

        IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());

        dataRepository.AddProduct(book);

        Assert.IsTrue(dataRepository.GetAllProducts().Contains(book));

        Assert.ThrowsException<Exception>(() => dataRepository.GetProduct("For sure not even a valid guid"));
    }

    [TestMethod]
    public void StateTests()
    {
        IProduct bookProduct = new Book("Buszujący w Zbożu", 10.0, " J.D. Salinger", "Albatros", 100, new DateTime(1951, 7, 16));

        const int amountOfBooks = 10;
        double wholeStockPrice = bookProduct.Price * amountOfBooks;

        IState bookProductState = new State(bookProduct, amountOfBooks);

        Assert.AreEqual(bookProduct, bookProductState.Product);
        Assert.AreEqual(amountOfBooks, bookProductState.Quantity);
        Assert.IsTrue((DateTime.Now - bookProductState.LastUpdatedDate).TotalSeconds < 1);
        Assert.AreEqual(wholeStockPrice, bookProductState.Product.Price * bookProductState.Quantity);
    }

    [TestMethod]
    public void BorrowEventTests()
    {
        // TODO: Implement BorrowEventTests

    }

    [TestMethod]
    public void ReturnEventTests()
    {
        // TODO: Implement ReturnEventTests
    }

    [TestMethod]
    public void DeliveryEventTests()
    {
        // TODO: Implement DeliveryEventTests
    }

    [TestMethod]
    public void DataContextTests()
    {
        DataContext dataContext = new DataContext();

        Assert.IsNotNull(dataContext.Users);
        Assert.IsNotNull(dataContext.Products);
        Assert.IsNotNull(dataContext.States);
        Assert.IsNotNull(dataContext.Events);
    }

    [TestMethod]
    public void DataRepositoryTests()
    {
        IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());

        IUser user = new User("John", "Doe", "Doe", 100.0, 1234567890, null);
        IProduct product = new Book("Book1", 10.0, "Author1", "Publisher1", 100, new DateTime(2022, 1, 1));
        IState state = new State(product, 10, "1234567890");

        dataRepository.AddUser(user);
        dataRepository.AddProduct(product);
        dataRepository.AddState(state);

        Assert.IsTrue(dataRepository.GetAllUsers().Contains(user));
        Assert.IsTrue(dataRepository.GetAllProducts().Contains(product));
        Assert.IsTrue(dataRepository.GetAllStates().Contains(state));

        Assert.AreEqual(user, dataRepository.GetUser(user.Guid));
        Assert.AreEqual(product, dataRepository.GetProduct(product.Guid));
        Assert.AreEqual(state, dataRepository.GetState(state.Guid));

        Assert.ThrowsException<Exception>(() => dataRepository.GetUser("For sure not even a valid guid"));
        Assert.ThrowsException<Exception>(() => dataRepository.GetProduct("For sure not even a valid guid"));
        Assert.ThrowsException<Exception>(() => dataRepository.GetState("For sure not even a valid guid"));

        dataRepository.RemoveUser(user.Guid);
        dataRepository.RemoveProduct(product.Guid);
        dataRepository.RemoveState(state.Guid);

        Assert.IsFalse(dataRepository.GetAllUsers().Contains(user));
        Assert.IsFalse(dataRepository.GetAllProducts().Contains(product));
        Assert.IsFalse(dataRepository.GetAllStates().Contains(state));
    }

    [TestMethod]
    public void DataFillerTests()
    {
        IDataContext dataContext = new DataContext();
        IDataFiller dataFiller = new PresetFiller();

        dataFiller.Fill(dataContext);

        Assert.AreEqual(5, dataContext.Users.Count);
        Assert.AreEqual(5, dataContext.Products.Count);
        Assert.AreEqual(5, dataContext.States.Count);
        Assert.AreEqual(7, dataContext.Events.Count);

        dataContext = new DataContext();
        dataFiller = new RandomFiller();

        dataFiller.Fill(dataContext);

        Assert.IsTrue(dataContext.Users.Count >= 5 && dataContext.Users.Count <= 11);
        Assert.IsTrue(dataContext.Products.Count >= 5 && dataContext.Products.Count <= 11);
        Assert.AreEqual(dataContext.Products.Count, dataContext.States.Count);
        Assert.IsTrue(dataContext.Events.Count >= 5 && dataContext.Events.Count <= 11);
    }
}
