using DataLayer.Implementations;
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
}
