using DataLayer.API;
using DataLayer.Implementations.Events;

namespace LogicLayer.API
{
    public interface IDataService
    {
        void BorrowProduct(IUser user, IState state);
        void ReturnProduct(IUser user, IState state);
        void DeliverProduct(IUser user, IState state, int amount);

        // Maybe add in the future
        // void RegisterUser(string firstName, string lastName, string email, double balance, int phoneNumber);
        // void RegisterProduct
        // void UpdateUserBalance
        // void UpdateProductPrice
        // bool CheckProductAvailability
    }
}