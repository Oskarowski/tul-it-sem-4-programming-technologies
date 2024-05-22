using DataLayer.API;
using DataLayer.Implementations.Events;
using LogicLayer.API;
using System;

namespace LogicLayer.Implementations
{
    public class DataService : IDataService
    {
        private IDataRepository _dataRepository;
        public static IDataService NewInstance(IDataRepository dataRepository)
        {
            return new DataService(dataRepository);
        }
        private DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void DeliverProduct(IUser user, IState state, int amount)
        {

            // Check if such product exists
            if (!_dataRepository.DoesProductExist(state.Product.Guid))
            {
                throw new Exception("Product not found");
            }

            if (!_dataRepository.DoesUserExist(user.Guid))
            {
                throw new Exception("User not found");
            }

            try
            {
                _dataRepository.AddEvent(new Delivery(user, state, amount));
            }
            catch (Exception e)
            {
                throw new Exception("Unable to perform deliver: " + e.Message, e); ;
            }

        }

        public void BorrowProduct(IUser user, IState state)
        {
            // Check if such product exists
            if (!_dataRepository.DoesProductExist(state.Product.Guid))
            {
                throw new Exception("Product not found");
            }
            if (!_dataRepository.DoesUserExist(user.Guid))
            {
                throw new Exception("User not found");
            }

            try
            {
                _dataRepository.AddEvent(new Borrow(user, state));
            }
            catch (Exception e)
            {
                throw new Exception("Unable to perform borrow: " + e.Message, e); ;
            }
        }
        public void ReturnProduct(IUser user, IState state)
        {
            // Check if such product exists
            if (!_dataRepository.DoesProductExist(state.Product.Guid))
            {
                throw new Exception("Product not found");
            }
            if (!_dataRepository.DoesUserExist(user.Guid))
            {
                throw new Exception("User not found");
            }

            try
            {
                _dataRepository.AddEvent(new Return(user, state));
            }
            catch (Exception e)
            {
                throw new Exception("Unable to perform return: " + e.Message, e); ;
            }
        }

    }
}