using PresentationLayer.Model.API;
using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Implementation
{
    internal class UserModelOperation : IUserModelOperation
    {
        private IUserCRUD _userCRUD;

        public UserModelOperation(IUserCRUD? userCrud)
        {
            this._userCRUD = userCrud ?? IUserCRUD.CreateUserCRUD();
        }

        private IUserModel Map(IUserDTO user)
        {
            return new UserModel(user.Guid, user.FirstName, user.LastName, user.Email, user.Balance, user.PhoneNumber);
        }

        public async Task AddAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            await this._userCRUD.AddUserAsync(guid, firstName, lastName, email, balance, phoneNumber);
        }

        public async Task<IUserModel> GetAsync(string guid)
        {
            return this.Map(await this._userCRUD.GetUserAsync(guid));
        }

        public async Task UpdateAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            await this._userCRUD.UpdateUserAsync(guid, firstName, lastName, email, balance, phoneNumber);
        }

        public async Task DeleteAsync(string guid)
        {
            await this._userCRUD.DeleteUserAsync(guid);
        }

        public async Task<Dictionary<string, IUserModel>> GetAllAsync()
        {
            Dictionary<string, IUserModel> result = new Dictionary<string, IUserModel>();

            foreach (IUserDTO user in (await this._userCRUD.GetAllUsersAsync()).Values)
            {
                result.Add(user.Guid, this.Map(user));
            }

            return result;
        }

        public async Task<int> GetCountAsync()
        {
            return await this._userCRUD.GetUsersCountAsync();
        }
    }
}
