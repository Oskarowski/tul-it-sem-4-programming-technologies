using DataLayer.Implementations;
using DataLayer.API;
using LogicLayer.Implementations;
using LogicLayer.API;

namespace Tests;

[TestClass]
public class LogicLayerTests
{
    [TestMethod]
    public void TestDeliverProduct_Success()
    {
        // Arrange
        IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());

        var testUser1 = new User("User 1", "1", "1@gmail.com", 1000, 1234567890, null);

        const int amountOfBooksOnStart = 5;
        IProduct bookProduct1 = new Book("Buszujący w Zbożu", 10.0, " J.D. Salinger", "Albatros", 258, new DateTime(1951, 7, 16));

        var stateOfBookProduct1 = new State(bookProduct1, amountOfBooksOnStart);

        dataRepository.AddUser(testUser1);
        dataRepository.AddProduct(bookProduct1);
        dataRepository.AddState(stateOfBookProduct1);

        IDataService dataService = new DataService(dataRepository);

        // Act
        const int amountOfBooksToDeliver = 5;
        dataService.DeliverProduct(testUser1, stateOfBookProduct1, amountOfBooksToDeliver);

        // Assert
        Assert.AreEqual(amountOfBooksOnStart + amountOfBooksToDeliver, stateOfBookProduct1.Quantity);
    }

    [TestMethod]
    public void TestBorrowProduct_Success()
    {
        // Arrange
        IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());

        var testUser1 = new User("User 1", "1", "1@gmail.com", 1000, 1234567890, null);

        const int amountOfBooksOnStart = 5;
        IProduct bookProduct1 = new Book("Buszujący w Zbożu", 10.0, " J.D. Salinger", "Albatros", 258, new DateTime(1951, 7, 16));

        var stateOfBookProduct1 = new State(bookProduct1, amountOfBooksOnStart);

        dataRepository.AddUser(testUser1);
        dataRepository.AddProduct(bookProduct1);
        dataRepository.AddState(stateOfBookProduct1);

        IDataService dataService = new DataService(dataRepository);

        // Act
        dataService.BorrowProduct(testUser1, stateOfBookProduct1);

        // Assert
        Assert.IsTrue(testUser1.ProductsDic.ContainsKey(bookProduct1.Guid));
        Assert.AreEqual(amountOfBooksOnStart - 1, stateOfBookProduct1.Quantity);
    }

    [TestMethod]
    public void TestReturnProduct_Success()
    {
        // Arrange
        IDataRepository dataRepository = IDataRepository.CreateDataRepository(new DataContext());

        var testUser1 = new User("User 1", "1", "1@gmail.com", 1000, 1234567890, null);

        const int amountOfBooksOnStart = 5;
        IProduct bookProduct1 = new Book("Buszujący w Zbożu", 10.0, " J.D. Salinger", "Albatros", 258, new DateTime(1951, 7, 16));

        var stateOfBookProduct1 = new State(bookProduct1, amountOfBooksOnStart);

        dataRepository.AddUser(testUser1);
        dataRepository.AddProduct(bookProduct1);
        dataRepository.AddState(stateOfBookProduct1);

        IDataService dataService = new DataService(dataRepository);

        dataService.BorrowProduct(testUser1, stateOfBookProduct1);

        // Act
        dataService.ReturnProduct(testUser1, stateOfBookProduct1);

        // Assert
        Assert.IsFalse(testUser1.ProductsDic.ContainsKey(bookProduct1.Guid));
        Assert.AreEqual(amountOfBooksOnStart, stateOfBookProduct1.Quantity);
    }
}