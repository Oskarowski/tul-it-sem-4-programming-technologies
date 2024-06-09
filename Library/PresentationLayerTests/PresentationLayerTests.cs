using Presentation;
using Presentation.Model.API;
using Presentation.ViewModel;
using PresentationLayerTests.MockClasses;
using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTests
{
    [TestClass]
    public class PresentationLayerTests
    {
        [TestMethod]
        public void UserMasterViewModelTest()
        {
            IUserCRUD fakeUserCrud = new MockUserCRUD();
            IUserModelOperation operation = IUserModelOperation.CreateModelOperation(fakeUserCrud);
            IUserMasterViewModel viewModel = IUserMasterViewModel.CreateViewModel(operation);

            viewModel.FirstName = "Alice";
            viewModel.LastName = "Jane";
            viewModel.Email = "AliceJane@mail.com";
            viewModel.Balance = 100;
            viewModel.PhoneNumber = "123456789";

            Assert.IsNotNull(viewModel.CreateUser);
            Assert.IsNotNull(viewModel.RemoveUser);
            Assert.IsTrue(viewModel.CreateUser.CanExecute(null));
            Assert.IsTrue(viewModel.RemoveUser.CanExecute(null));
        }

        [TestMethod]
        public void UserDetailViewModelTests()
        {
            IUserCRUD mockUserCrud = new MockUserCRUD();
            IUserModelOperation operation = IUserModelOperation.CreateModelOperation(mockUserCrud);
            IUserDetailViewModel viewModel = IUserDetailViewModel.CreateViewModel("1", "Alice", "Jane", "Test@test.mail", 100, "123456789", operation);
            
            Assert.AreEqual("1", viewModel.Guid);
            Assert.AreEqual("Alice", viewModel.FirstName);
            Assert.AreEqual("Jane", viewModel.LastName);
            Assert.AreEqual("Test@test.mail", viewModel.Email);
            Assert.AreEqual(100, viewModel.Balance);
            Assert.AreEqual("123456789", viewModel.PhoneNumber);
            Assert.IsTrue(viewModel.UpdateUser.CanExecute(null));
        }

        [TestMethod]
        public void ProductMasterViewModelTests()
        {
            IProductCRUD mockProductCrud = new MockProductCRUD();
            IProductModelOperation operation = IProductModelOperation.CreateModelOperation(mockProductCrud);
            IProductMasterViewModel viewModel = IProductMasterViewModel.CreateViewModel(operation);

            viewModel.Name = "Product";
            viewModel.Price = 100;
            viewModel.Author = "Author";
            viewModel.Publisher = "Publisher";
            viewModel.Pages = 100;
            viewModel.PublicationDate = DateTime.Now;

            Assert.IsNotNull(viewModel.CreateProduct);
            Assert.IsNotNull(viewModel.RemoveProduct);
            Assert.IsTrue(viewModel.CreateProduct.CanExecute(null));
            Assert.IsTrue(viewModel.RemoveProduct.CanExecute(null));
        }

        [TestMethod]
        public void ProductDetailViewModelTests()
        {
            IProductCRUD mockProductCrud = new MockProductCRUD();
            IProductModelOperation operation = IProductModelOperation.CreateModelOperation(mockProductCrud);
            IProductDetailViewModel viewModel = IProductDetailViewModel.CreateViewModel("1", "Product", 100, "Author", "Publisher", 100, new DateTime(2020,12,12), operation);

            Assert.AreEqual("1", viewModel.Guid);
            Assert.AreEqual("Product", viewModel.Name);
            Assert.AreEqual(100, viewModel.Price);
            Assert.AreEqual("Author", viewModel.Author);
            Assert.AreEqual("Publisher", viewModel.Publisher);
            Assert.AreEqual(100, viewModel.Pages);
            Assert.AreEqual(new DateTime(2020,12,12), viewModel.PublicationDate);
            Assert.IsTrue(viewModel.UpdateProduct.CanExecute(null));
        }

        [TestMethod]
        public void StateMasterViewModelTests()
        {
            IStateCRUD mockStateCrud = new MockStateCRUD();
            IStateModelOperation operation = IStateModelOperation.CreateModelOperation(mockStateCrud);
            IStateMasterViewModel viewModel = IStateMasterViewModel.CreateViewModel(operation);  

            viewModel.ProductGuid = "1";
            viewModel.Quantity = 10;

            Assert.IsNotNull(viewModel.CreateState);
            Assert.IsNotNull(viewModel.RemoveState);
            Assert.IsTrue(viewModel.CreateState.CanExecute(null));
            Assert.IsTrue(viewModel.RemoveState.CanExecute(null));
        }

        [TestMethod]
        public void StateDetailViewModelTests()
        {
            IStateCRUD mockStateCrud = new MockStateCRUD();
            IStateModelOperation operation = IStateModelOperation.CreateModelOperation(mockStateCrud);
            IStateDetailViewModel viewModel = IStateDetailViewModel.CreateViewModel("1", "1", 10, operation);

            Assert.AreEqual("1", viewModel.Guid);
            Assert.AreEqual("1", viewModel.ProductGuid);
            Assert.AreEqual(10, viewModel.Quantity);
            Assert.IsTrue(viewModel.UpdateState.CanExecute(null));
        }

        [TestMethod]
        public void EventMasterViewModelTests()
        {
            IEventCRUD mockEventCrud = new MockEventCRUD();
            IEventModelOperation operation = IEventModelOperation.CreateModelOperation(mockEventCrud);
            IEventMasterViewModel viewModel = IEventMasterViewModel.CreateViewModel(operation);

            viewModel.StateGuid = "1";
            viewModel.UserGuid = "1";

            Assert.IsNotNull(viewModel.PurchaseEvent);
            Assert.IsNotNull(viewModel.SupplyEvent);
            Assert.IsNotNull(viewModel.ReturnEvent);
            Assert.IsNotNull(viewModel.RemoveEvent);
            Assert.IsTrue(viewModel.PurchaseEvent.CanExecute(null));
            Assert.IsTrue(viewModel.SupplyEvent.CanExecute(null));
            Assert.IsTrue(viewModel.ReturnEvent.CanExecute(null));
            Assert.IsTrue(viewModel.RemoveEvent.CanExecute(null));
        }

        [TestMethod]
        public void EventDetailViewModelTests()
        {
            IEventCRUD mockEventCrud = new MockEventCRUD();
            IEventModelOperation operation = IEventModelOperation.CreateModelOperation(mockEventCrud);
            IEventDetailViewModel viewModel = IEventDetailViewModel.CreateViewModel("1", "1", "1", new DateTime(2020,12,12), "Delivery", operation);

            Assert.AreEqual("1", viewModel.Guid);
            Assert.AreEqual("1", viewModel.StateGuid);
            Assert.AreEqual("1", viewModel.UserGuid);
            Assert.AreEqual(new DateTime(2020,12,12), viewModel.CreatedAt);
            Assert.AreEqual("Delivery", viewModel.Type);
            Assert.IsTrue(viewModel.UpdateEvent.CanExecute(null));
        }
    }
}
