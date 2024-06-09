using Presentation.Model.API;
using Presentation.ViewModel;
using Presentation;
using PresentationLayerTests.MockClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTests
{
    internal class RandomSeeder : ISeeder
    { 
        public void GenerateUserModels(IUserMasterViewModel viewModel)
        {
            IUserModelOperation operation = IUserModelOperation.CreateModelOperation(new MockUserCRUD());

            for (int i = 0; i < 10; i++)
            {
                viewModel.Users.Add(IUserDetailViewModel.CreateViewModel(i.ToString(), RandomString(10), RandomString(10),
                                    RandomString(10) + "@" + RandomString(10), RandomNumber(1, 101), RandomPhoneNumber(),
                                    operation));
            }
        }

        public void GenerateProductModels(IProductMasterViewModel viewModel)
        {
            IProductModelOperation operation = IProductModelOperation.CreateModelOperation(new MockProductCRUD());

            for (int i = 0; i < 5; i++)
            {
                viewModel.Products.Add(IProductDetailViewModel.CreateViewModel(i.ToString(), RandomString(10), RandomNumber(1, 101), RandomString(10), RandomString(10), RandomNumber(1, 101), RandomDateTime(), operation));
            }
        }

        public void GenerateStateModels(IStateMasterViewModel viewModel)
        {
            IStateModelOperation operation = IStateModelOperation.CreateModelOperation(new MockStateCRUD());

            for (int i = 0; i < 5; i++)
            {
                viewModel.States.Add(IStateDetailViewModel.CreateViewModel(i.ToString(), i.ToString(), RandomNumber(1, 101), operation));
            }
        }

        public void GenerateEventModels(IEventMasterViewModel viewModel)
        {
            IEventModelOperation operation = IEventModelOperation.CreateModelOperation(new MockEventCRUD());

            for (int i = 0; i < 5; i++)
            {
                viewModel.Events.Add(IEventDetailViewModel.CreateViewModel(i.ToString(), i.ToString(), i.ToString(), DateTime.Now, "Delivery", operation));
            }
        }

        private readonly Random _random = new Random();

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        private string RandomPhoneNumber()
        {
            return RandomNumber(100000000, 999999999).ToString();
        }

        private DateTime RandomDateTime()
        {
            DateTime start = new DateTime(1901, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }
    }
}
