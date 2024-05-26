﻿using Service.API;
using Service.Implementation;

namespace PresentationLayer.Model.API
{
    public interface IUserModelOperation
    {
        static IUserModelOperation CreateModelOperation(IUserCRUD? userCrud = null)
        {
            return new UserModelOperation(userCrud) ?? IUserCRUD.CreateUserCRUD();
        }

        Task AddAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber);

        Task<IUserModel> GetAsync(string guid);

        Task UpdateAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber);

        Task DeleteAsync(string guid);

        Task<Dictionary<string, IUserModel>> GetAllAsync();

        Task<int> GetCountAsync();
    }
}