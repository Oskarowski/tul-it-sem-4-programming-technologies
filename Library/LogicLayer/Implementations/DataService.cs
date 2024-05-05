using DataLayer.API;
using DataLayer.Implementations.Events;
using LogicLayer.API;

namespace LogicLayer.Implementations
{
    public class DataService : IDataService
    {
        private IDataRepository _dataRepository;
        private DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public static IDataService CreateDataService(IDataRepository dataRepository)
        {
            return new DataService(dataRepository);
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
                _dataRepository.AddEvent(Delivery.CreateDelivery(user, state, amount));
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
                _dataRepository.AddEvent(Borrow.CreateBorrow(user, state));
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
                _dataRepository.AddEvent(Return.CreateReturn(user, state));
            }
            catch (Exception e)
            {
                throw new Exception("Unable to perform return: " + e.Message, e); ;
            }
        }

    }
}