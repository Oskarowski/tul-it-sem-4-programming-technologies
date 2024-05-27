using Presentation;
using Presentation.Model.API;
using Presentation.ViewModel;
using Presentationion.ViewModel;
using PresentationLayerTests.MockClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTests
{
    internal class PresetSeeder : ISeeder
    {
        private IErrorInformer _informer = new MockErrorInformer();

        public void GenerateUserModels(IUserMasterViewModel viewModel)
        {
            IUserModelOperation operation = IUserModelOperation.CreateModelOperation(new MockUserCRUD());

            viewModel.Users.Add(IUserDetailViewModel.CreateViewModel("1", "Alice", "Jane", "alice@example.com", 12, "123456789", operation, _informer));
            viewModel.Users.Add(IUserDetailViewModel.CreateViewModel("2", "Bob", "Smith", "bob@example.com", 34, "987654321", operation, _informer));
            viewModel.Users.Add(IUserDetailViewModel.CreateViewModel("3", "Charlie", "Brown", "charlie@example.com", 56, "456123789", operation, _informer));
            viewModel.Users.Add(IUserDetailViewModel.CreateViewModel("4", "Diana", "Prince", "diana@example.com", 78, "321654987", operation, _informer));
            viewModel.Users.Add(IUserDetailViewModel.CreateViewModel("5", "Eve", "Adams", "eve@example.com", 90, "654789123", operation, _informer));
        }

        public void GenerateProductModels(IProductMasterViewModel viewModel)
        {
            IProductModelOperation operation = IProductModelOperation.CreateModelOperation(new MockProductCRUD());

            viewModel.Products.Add(IProductDetailViewModel.CreateViewModel("1", "Old Man and the Sea", 100, "Ernest Hemingway", "Publisher1", 100, new DateTime(1972, 12, 12), operation, _informer));
            viewModel.Products.Add(IProductDetailViewModel.CreateViewModel("2", "The Great Gatsby", 200, "F. Scott Fitzgerald", "Publisher2", 200, new DateTime(1982, 11, 11), operation, _informer));
            viewModel.Products.Add(IProductDetailViewModel.CreateViewModel("3", "The Catcher in the Rye", 300, "J.D. Salinger", "Publisher3", 300, new DateTime(1992, 10, 10), operation, _informer));
            viewModel.Products.Add(IProductDetailViewModel.CreateViewModel("4", "To Kill a Mockingbird", 400, "Harper Lee", "Publisher4", 400, new DateTime(2002, 9, 9), operation, _informer));
            viewModel.Products.Add(IProductDetailViewModel.CreateViewModel("5", "1984", 500, "George Orwell", "Publisher5", 500, new DateTime(2012, 8, 8), operation, _informer));
        }

        public void GenerateStateModels(IStateMasterViewModel viewModel)
        {
            IStateModelOperation operation = IStateModelOperation.CreateModelOperation(new MockStateCRUD());

            viewModel.States.Add(IStateDetailViewModel.CreateViewModel("1", "1", 10, operation, _informer));
            viewModel.States.Add(IStateDetailViewModel.CreateViewModel("2", "2", 20, operation, _informer));
            viewModel.States.Add(IStateDetailViewModel.CreateViewModel("3", "3", 30, operation, _informer));
            viewModel.States.Add(IStateDetailViewModel.CreateViewModel("4", "4", 40, operation, _informer));
            viewModel.States.Add(IStateDetailViewModel.CreateViewModel("5", "5", 50, operation, _informer));
        }

        public void GenerateEventModels(IEventMasterViewModel viewModel)
        {
            IEventModelOperation operation = IEventModelOperation.CreateModelOperation(new MockEventCRUD());

            viewModel.Events.Add(IEventDetailViewModel.CreateViewModel("1", "1", "1", DateTime.Now, "Return", operation, _informer));
            viewModel.Events.Add(IEventDetailViewModel.CreateViewModel("2", "2", "2", DateTime.Now, "Delivery", operation, _informer));
            viewModel.Events.Add(IEventDetailViewModel.CreateViewModel("3", "3", "3", DateTime.Now, "Borrow", operation, _informer));
            viewModel.Events.Add(IEventDetailViewModel.CreateViewModel("4", "4", "4", DateTime.Now, "Delivery", operation, _informer));
            viewModel.Events.Add(IEventDetailViewModel.CreateViewModel("5", "5", "5", DateTime.Now, "Borrow", operation, _informer));
            viewModel.Events.Add(IEventDetailViewModel.CreateViewModel("6", "3", "5", DateTime.Now, "Delivery", operation, _informer));
        }
    }
}
