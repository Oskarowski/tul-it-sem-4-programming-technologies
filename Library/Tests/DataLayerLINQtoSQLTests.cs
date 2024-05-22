using System.Linq;
using DataLayer;
using DataLayer.API;
using DataLayer.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Data.Linq;
using System.IO;
using System;

namespace Tests
{

    [TestClass]
    [DeploymentItem(@"Instrumentation\Library.mdf", @"Instrumentation")]
    public class DataLayerLINQtoSQLTests
    { 
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            string _DBRelativePath = @"Instrumentation\Library.mdf";
            string _TestingWorkingFolder = Environment.CurrentDirectory;
            string _DBPath = Path.Combine(_TestingWorkingFolder, _DBRelativePath);
            Console.WriteLine($"DB Path: {_DBPath}");
            FileInfo _databaseFile = new FileInfo(_DBPath);
            Assert.IsTrue(_databaseFile.Exists, $"{Environment.CurrentDirectory}");
            m_ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Wojtek\Documents\Studia\Programming Technologies\tul-it-sem-4-programming-technologies\Library\Tests\Instrumentation\Library.mdf;Integrated Security=True";
        }

        [TestMethod]
        public void CatalogFillQueryTest()
        {
            using (CatalogueDataContext _catalogue = new CatalogueDataContext(m_ConnectionString))
            {
                Assert.IsNotNull(_catalogue.Connection);
                Assert.AreEqual<int>(0, _catalogue.Users.Count());
                Assert.AreEqual<int>(0, _catalogue.Products.Count());
                Assert.AreEqual<int>(0, _catalogue.States.Count());
                Assert.AreEqual<int>(0, _catalogue.Events.Count());
                try
                {

                    ExecuteSqlScript(_catalogue, @"Instrumentation\DatabaseSeeder.sql");
                    Assert.AreEqual<int>(3, _catalogue.Users.Count());
                    Assert.AreEqual<int>(3, _catalogue.Products.Count());
                    Assert.AreEqual<int>(3, _catalogue.States.Count());
                    Assert.AreEqual<int>(3, _catalogue.Events.Count());
                }
                finally
                {
                    ExecuteSqlScript(_catalogue, @"Instrumentation\ClearData.sql");
                }
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            using (CatalogueDataContext _catalogue = new CatalogueDataContext(m_ConnectionString))
            {
                Assert.IsNotNull(_catalogue.Connection);
                Assert.AreEqual<int>(0, _catalogue.Users.Count());
                Assert.AreEqual<int>(0, _catalogue.Products.Count());
                Assert.AreEqual<int>(0, _catalogue.States.Count());
                Assert.AreEqual<int>(0, _catalogue.Events.Count());
                try
                {
                    Guid userGuid = new Guid();
                    Guid bookGuid = new Guid();
                    Guid stateGuid = new Guid();
                    Guid eventGuid = new Guid();
                    _catalogue.InsertUser("John", "Doe", "JohnDoe@email.com", "123456789", userGuid);
                    _catalogue.InsertBook("Book1", 10.00m, "Author1", "Publisher1", 100, new DateTime(2022, 1, 1), bookGuid);
                    _catalogue.InsertState(bookGuid, 10, stateGuid);
                    _catalogue.InsertEvent(userGuid, bookGuid, "Borrow", null, eventGuid);

                    Assert.AreEqual<int>(1, _catalogue.Users.Count());
                    Assert.AreEqual<int>(1, _catalogue.Products.Count());
                    Assert.AreEqual<int>(1, _catalogue.States.Count());
                    Assert.AreEqual<int>(1, _catalogue.Events.Count());
                }
                finally
                {
                    ExecuteSqlScript(_catalogue, @"Instrumentation\ClearData.sql");
                }

            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            //TODO: NOT WORKING YET
            using (CatalogueDataContext _catalogue = new CatalogueDataContext(m_ConnectionString))
            {
                Assert.IsNotNull(_catalogue.Connection);
                Assert.AreEqual<int>(0, _catalogue.Users.Count());
                Assert.AreEqual<int>(0, _catalogue.Products.Count());
                Assert.AreEqual<int>(0, _catalogue.States.Count());
                Assert.AreEqual<int>(0, _catalogue.Events.Count());
                try
                {
                    ExecuteSqlScript(_catalogue, @"Instrumentation\DatabaseSeeder.sql");
                    Assert.AreEqual<int>(3, _catalogue.Users.Count());
                    Assert.AreEqual<int>(3, _catalogue.Products.Count());
                    Assert.AreEqual<int>(3, _catalogue.States.Count());
                    Assert.AreEqual<int>(3, _catalogue.Events.Count());

                    //_catalogue.DeleteUser("JohnDoe");
                    //_catalogue.DeleteProduct("Book1");
                    //_catalogue.DeleteState("Book1");
                    //_catalogue.DeleteEvent("JohnDoe");
                    _catalogue.DeleteUsersBy(_catalogue.Users.First().ID);
                    _catalogue.DeleteProductsBy(_catalogue.Products.First().ID);
                    _catalogue.DeleteStatesBy(_catalogue.States.First().ID);
                    _catalogue.DeleteEventsBy(_catalogue.Events.First().ID);

                    Assert.AreEqual<int>(2, _catalogue.Users.Count());
                    Assert.AreEqual<int>(2, _catalogue.Products.Count());
                    Assert.AreEqual<int>(2, _catalogue.States.Count());
                    Assert.AreEqual<int>(2, _catalogue.Events.Count());
                }
                finally
                {
                    ExecuteSqlScript(_catalogue, @"Instrumentation\ClearData.sql");
                }
            }
        }

        private void ExecuteSqlScript(CatalogueDataContext context, string scriptPath)
        {
            string script = File.ReadAllText(scriptPath);
            context.ExecuteCommand(script);
        }
        private static string m_ConnectionString;
    }
}
