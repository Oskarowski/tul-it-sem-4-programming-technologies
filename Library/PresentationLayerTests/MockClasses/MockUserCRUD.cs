using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTests.MockClasses
{
    internal class MockUserCRUD : IUserCRUD
    {
        private MockRepository mockRepository = new MockRepository();

        public async Task AddUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            await mockRepository.AddUserAsync(guid, firstName, lastName, email, balance, phoneNumber);
        }

        public async Task<IUserDTO> GetUserAsync(string guid)
        {
            return await mockRepository.GetUserAsync(guid);
        }

        public async Task UpdateUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            await mockRepository.UpdateUserAsync(guid, firstName, lastName, email, balance, phoneNumber);
        }

        public async Task DeleteUserAsync(string guid)
        {
            await mockRepository.DeleteUserAsync(guid);
        }

        public async Task<Dictionary<string, IUserDTO>> GetAllUsersAsync()
        {
            return await mockRepository.GetAllUsersAsync();
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await mockRepository.GetUsersCountAsync();
        }
    }
}
