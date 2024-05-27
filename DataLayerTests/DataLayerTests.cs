using DataLayer.API;
using Microsoft.Data.SqlClient;

namespace DataLayerTests
{
    [TestClass]
    [DeploymentItem("MockDB.mdf")] // DB is copied to the deployment directory where the test is executed.
    public class DataLayerTests
    {
        private static string connectionString;

        // private readonly IDataRepository _dataRepository = IDataRepository.NewInstance(IDataContext.NewInstance(connectionString));

        private IDataRepository _dataRepository;


        [ClassInitialize]
        public static void ClassInitializeMethod(TestContext context)
        {
            string _DBRelativePath = @"MockDB.mdf";
            string _projectRootDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string _DBPath = Path.Combine(_projectRootDir, _DBRelativePath);
            FileInfo _databaseFile = new FileInfo(_DBPath);
            Assert.IsTrue(_databaseFile.Exists, $"{Environment.CurrentDirectory}");

            connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
        }

        [TestInitialize]
        public void TestInitialize()
        {
            IDataContext dataContext = IDataContext.NewInstance(connectionString);
            _dataRepository = IDataRepository.NewInstance(dataContext);
        }

        [TestMethod]
        public void TestDatabaseConnection()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Assert.IsTrue(connection.State == System.Data.ConnectionState.Open, "Connection to the database failed.");
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Exception occurred while connecting to the database: {ex.Message}");
                }
            }
        }

        [TestMethod]
        public async Task UserTests()
        {
            string userGuid = "cfaf2913-3677-4c56-a1cd-fa1647";

            // Add a new user
            string firstName = "John";
            string lastName = "Wick";
            string email = "johnwick@continental.com";
            double balance = 1000000;
            string phoneNumber = "315-194-6020";
            await _dataRepository.AddUserAsync(userGuid, firstName, lastName, email, balance, phoneNumber);

            IUser retrivedUser = await _dataRepository.GetUserAsync(userGuid);

            // Assert that the user is correctly retrieved and the data is correct
            Assert.IsNotNull(retrivedUser);
            Assert.AreEqual(userGuid, retrivedUser.Guid);
            Assert.AreEqual(firstName, retrivedUser.FirstName);
            Assert.AreEqual(email, retrivedUser.Email);
            Assert.AreEqual(balance, retrivedUser.Balance);
            Assert.AreEqual(phoneNumber, retrivedUser.PhoneNumber);

            Assert.IsNotNull(await _dataRepository.GetAllUsersAsync());
            Assert.IsTrue(await _dataRepository.GetUsersCountAsync() > 0);

            string notExistingUserGuid = "afaf2913-1234-4c56-a1cd-fa1647";
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.GetUserAsync(notExistingUserGuid));

            // John Wick moves to a new place to start a new life
            string newFirstName = "Tom";
            string newLastName = "Hanks";
            string newEmail = "notjohnwick@continental.com";
            double newBalance = 9000000;
            string newPhoneNumber = "1-951-239-0523";
            await _dataRepository.UpdateUserAsync(userGuid, newFirstName, newLastName, newEmail, newBalance, newPhoneNumber);

            // Retrieve the updated data of now not John Wick
            IUser updatedUser = await _dataRepository.GetUserAsync(userGuid);

            // Assert that the the identity change was successful
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual(newFirstName, updatedUser.FirstName);
            Assert.AreEqual(newLastName, updatedUser.LastName);
            Assert.AreEqual(newEmail, updatedUser.Email);
            Assert.AreEqual(newBalance, updatedUser.Balance);
            Assert.AreEqual(newPhoneNumber, updatedUser.PhoneNumber);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.UpdateUserAsync(notExistingUserGuid,
                "John", "Doe", "johndow@wp.pl", 666, "123321123"));

            // John Wick disappears from the system
            await _dataRepository.DeleteUserAsync(userGuid);

            // Assert that an exception is thrown when trying to retrieve the deleted user
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.GetUserAsync(userGuid));

            // Assert that an exception is thrown when trying to delete the already deleted user
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.DeleteUserAsync(userGuid));
        }
    }
}
