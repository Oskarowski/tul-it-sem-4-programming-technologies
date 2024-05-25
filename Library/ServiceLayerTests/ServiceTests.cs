using DataLayer.API;
using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ServiceTests
{
    [TestClass]
    public class ServiceTests
    {
        private readonly IDataRepository _dataRepository = new MockRepository();

        [TestMethod]
        public async Task ServiceTest_User()
        {
            IUserCRUD userCRUD = IUserCRUD.CreateUserCRUD(_dataRepository);
            await userCRUD.AddUserAsync("1", "John", "Doe", "JohnDoe@mail", 100, "123456789");
            IUserDTO user = await userCRUD.GetUserAsync("1");

            Assert.IsNotNull(user);
            Assert.AreEqual("1", user.Guid);
            Assert.AreEqual("John", user.FirstName);
            Assert.AreEqual("Doe", user.LastName);
            Assert.AreEqual("JohnDoe@mail", user.Email);
            Assert.AreEqual(100, user.Balance);
            Assert.AreEqual("123456789", user.PhoneNumber);

            Assert.IsNotNull(await userCRUD.GetAllUsersAsync());
            Assert.AreEqual(1, await userCRUD.GetUsersCountAsync());

            await userCRUD.UpdateUserAsync("1", "Jane", "Doe", "JaneDoe@mail", 200, "987654321");
            IUserDTO updatedUser = await userCRUD.GetUserAsync("1");
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual("1", updatedUser.Guid);
            Assert.AreEqual("Jane", updatedUser.FirstName);
            Assert.AreEqual("Doe", updatedUser.LastName);
            Assert.AreEqual("JaneDoe@mail", updatedUser.Email);
            Assert.AreEqual(200, updatedUser.Balance);
            Assert.AreEqual("987654321", updatedUser.PhoneNumber);

            await userCRUD.DeleteUserAsync("1");
        }

        [TestMethod]
        public async Task ServiceTest_Product()
        {
            IProductCRUD productCRUD = IProductCRUD.CreateBookCRUD(_dataRepository);
            await productCRUD.AddProductAsync("1", "Product1", 100, "Steve", "StevePublishingHouse", 200, new DateTime(2020, 12, 12));
            IProductDTO product = await productCRUD.GetProductAsync("1");

            Assert.IsNotNull(product);
            Assert.AreEqual("1", product.Guid);
            Assert.AreEqual("Product1", product.Name);
            Assert.AreEqual(100, product.Price);
            Assert.AreEqual("Steve", product.Author);
            Assert.AreEqual("StevePublishingHouse", product.Publisher);
            Assert.AreEqual(200, product.Pages);
            Assert.AreEqual(new DateTime(2020, 12, 12), product.PublicationDate);

            Assert.IsNotNull(await productCRUD.GetAllProductsAsync());
            Assert.AreEqual(1, await productCRUD.GetProductsCountAsync());

            await productCRUD.UpdateProductAsync("1", "Book2", 200, "Bob", "BobPublishing", 150, new DateTime(2015, 12, 12));
            IProductDTO updatedProduct = await productCRUD.GetProductAsync("1");
            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual("1", updatedProduct.Guid);
            Assert.AreEqual("Book2", updatedProduct.Name);
            Assert.AreEqual(200, updatedProduct.Price);
            Assert.AreEqual("Bob", updatedProduct.Author);
            Assert.AreEqual("BobPublishing", updatedProduct.Publisher);
            Assert.AreEqual(150, updatedProduct.Pages);
            Assert.AreEqual(new DateTime(2015, 12, 12), updatedProduct.PublicationDate);

            await productCRUD.DeleteProductAsync("1");
        }

        [TestMethod]
        public async Task ServiceTest_State()
        {
            IStateCRUD stateCRUD = IStateCRUD.CreateStateCRUD(_dataRepository);
            await stateCRUD.AddStateAsync("1", "1", 10);
            IStateDTO state = await stateCRUD.GetStateAsync("1");

            Assert.IsNotNull(state);
            Assert.AreEqual("1", state.Guid);
            Assert.AreEqual("1", state.ProductGuid);
            Assert.AreEqual(10, state.Quantity);

            Assert.IsNotNull(await stateCRUD.GetAllStatesAsync());
            Assert.AreEqual(1, await stateCRUD.GetStatesCountAsync());

            await stateCRUD.UpdateStateAsync("1", "1", 20);
            IStateDTO updatedState = await stateCRUD.GetStateAsync("1");
            Assert.IsNotNull(updatedState);
            Assert.AreEqual("1", updatedState.Guid);
            Assert.AreEqual("1", updatedState.ProductGuid);
            Assert.AreEqual(20, updatedState.Quantity);

            await stateCRUD.DeleteStateAsync("1");
        }

        [TestMethod]
        public async Task ServiceTest_Event()
        {
            IEventCRUD eventCRUD = IEventCRUD.CreateEventCRUD(_dataRepository);
            await eventCRUD.AddEventAsync("1", "1", "1", new DateTime(2020, 12, 12), "Type1");
            IEventDTO eventDTO = await eventCRUD.GetEventAsync("1");

            Assert.IsNotNull(eventDTO);
            Assert.AreEqual("1", eventDTO.Guid);
            Assert.AreEqual("1", eventDTO.StateGuid);
            Assert.AreEqual("1", eventDTO.UserGuid);
            Assert.AreEqual(new DateTime(2020, 12, 12), eventDTO.CreatedAt);
            Assert.AreEqual("Type1", eventDTO.Type);

            Assert.IsNotNull(await eventCRUD.GetAllEventsAsync());
            Assert.AreEqual(1, await eventCRUD.GetEventsCountAsync());

            await eventCRUD.UpdateEventAsync("1", "2", "2", new DateTime(2019, 12, 12), "Type2");
            IEventDTO updatedEvent = await eventCRUD.GetEventAsync("1");
            Assert.IsNotNull(updatedEvent);
            Assert.AreEqual("1", updatedEvent.Guid);
            Assert.AreEqual("2", updatedEvent.StateGuid);
            Assert.AreEqual("2", updatedEvent.UserGuid);
            Assert.AreEqual(new DateTime(2019, 12, 12), updatedEvent.CreatedAt);
            Assert.AreEqual("Type2", updatedEvent.Type);

            await eventCRUD.DeleteEventAsync("1");
        }
    }
}
