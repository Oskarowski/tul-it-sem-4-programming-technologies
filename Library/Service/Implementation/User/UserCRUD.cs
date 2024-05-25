using Service.API;
using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class UserCRUD : IUserCRUD
    {
        private IDataRepository _dataRepository;

        public UserCRUD(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IUserDTO Map(IUser user)
        {
            return new UserDTO(user.Guid, user.FirstName, user.LastName, user.Email, user.Balance, user.PhoneNumber);
        }

        public async Task AddUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            await _dataRepository.AddUserAsync(guid, firstName, lastName, email, balance, phoneNumber);
        }

        public async Task<IUserDTO> GetUserAsync(string guid)
        {
            return Map(await _dataRepository.GetUserAsync(guid));
        }

        public async Task UpdateUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            await _dataRepository.UpdateUserAsync(guid, firstName, lastName, email, balance, phoneNumber);
        }

        public async Task DeleteUserAsync(string guid)
        {
            await _dataRepository.DeleteUserAsync(guid);
        }

        public async Task<Dictionary<string, IUserDTO>> GetAllUsersAsync()
        {
            Dictionary<string, IUserDTO> result = new Dictionary<string, IUserDTO>();

            foreach (IUser user in (await _dataRepository.GetAllUsersAsync()).Values)
            {
                result.Add(user.Guid, this.Map(user));
            }

            return result;
        }

        public async Task<int> GetUsersCountAsync()
        {
            return (await _dataRepository.GetAllUsersAsync()).Count;
        }
    }
}
