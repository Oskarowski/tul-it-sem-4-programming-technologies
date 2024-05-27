using DataLayer.API;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IUserCRUD
    {
        static IUserCRUD CreateUserCRUD(IDataRepository? dataRepository = null)
        {
            return new UserCRUD(dataRepository ?? IDataRepository.CreateDatabase());
        }

        Task AddUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber);
        
        Task<IUserDTO> GetUserAsync(string guid);
        
        Task UpdateUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber);
        
        Task DeleteUserAsync(string guid);
        
        Task<Dictionary<string, IUserDTO>> GetAllUsersAsync();
        
        Task<int> GetUsersCountAsync();
    }
}
