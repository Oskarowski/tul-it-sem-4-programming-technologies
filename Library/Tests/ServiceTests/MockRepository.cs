using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ServiceTests
{
    internal class MockRepository : IDataRepository
    {
        public Dictionary<string, IUser> Users { get; set; } = new Dictionary<string, IUser>();
        public Dictionary<string, IEvent> Events { get; set; } = new Dictionary<string, IEvent>();
        public Dictionary<string, IState> States { get; set; } = new Dictionary<string, IState>();
        public Dictionary<string, IBook> Products { get; set; } = new Dictionary<string, IBook>();

        public void Seed(IDataFiller dataSeeder)
        {
            throw new NotImplementedException();
        }
        #region User CRUD

        public async Task AddUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            Users.Add(guid, new MockUser(guid, firstName, lastName, email, balance, phoneNumber));
        }

        public async Task<IUser> GetUserAsync(string guid)
        {
            return await Task.FromResult(Users[guid]);
        }

        public async Task UpdateUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            Users[guid].FirstName = firstName;
            Users[guid].LastName = lastName;
            Users[guid].Email = email;
            Users[guid].Balance = balance;
            Users[guid].PhoneNumber = phoneNumber;
        }

        public async Task DeleteUserAsync(string guid)
        {
            Users.Remove(guid);
        }

        public async Task<Dictionary<string, IUser>> GetAllUsersAsync()
        {
            return await Task.FromResult(Users);
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await Task.FromResult(Users.Count);
        }

        #endregion


        #region Event CRUD

        public async Task AddEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            Events.Add(guid, new MockEvent(guid, userGuid, stateGuid, type, createdAt));
        }

        public async Task<IEvent> GetEventAsync(string guid)
        {
            return await Task.FromResult(Events[guid]);
        }

        public async Task UpdateEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            Events[guid].StateGuid = stateGuid;
            Events[guid].UserGuid = userGuid;
            Events[guid].CreatedAt = createdAt;
            Events[guid].Type = type;
        }

        public async Task DeleteEventAsync(string guid)
        {
            Users.Remove(guid);
        }

        public async Task<Dictionary<string, IEvent>> GetAllEventsAsync()
        {
            return await Task.FromResult(Events);
        }

        public async Task<int> GetEventsCountAsync()
        {
            return await Task.FromResult(Events.Count);
        }

        #endregion


        #region State CRUD

        public async Task AddStateAsync(string guid, string productGuid, int quantity)
        {
            States.Add(guid, new MockState(guid, quantity, productGuid));
        }

        public async Task<IState> GetStateAsync(string guid)
        {
            return await Task.FromResult(States[guid]);
        }

        public async Task UpdateStateAsync(string guid, string productGuid, int quantity)
        {
            States[guid].ProductGuid = productGuid;
            States[guid].Quantity = quantity;
        }

        public async Task DeleteStateAsync(string guid)
        {
            States.Remove(guid);
        }

        public async Task<Dictionary<string, IState>> GetAllStatesAsync()
        {
            return await Task.FromResult(States);
        }

        public async Task<int> GetStatesCountAsync()
        {
            return await Task.FromResult(States.Count);
        }

        #endregion


        #region Product CRUD

        public async Task AddProductAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            Products.Add(guid, new MockBook(guid, name, price, author, publisher, pages, publicationDate));
        }

        public async Task<IBook> GetProductAsync(string guid)
        {
            return await Task.FromResult(Products[guid]);
        }

        public async Task UpdateProductAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            Products[guid].Name = name;
            Products[guid].Price = price;
            Products[guid].Author = author;
            Products[guid].Publisher = publisher;
            Products[guid].Pages = pages;
            Products[guid].PublicationDate = publicationDate;
        }

        public async Task DeleteProductAsync(string guid)
        {
            Products.Remove(guid);
        }

        public async Task<Dictionary<string, IBook>> GetAllProductsAsync()
        {
            return await Task.FromResult(Products);
        }

        public async Task<int> GetProductsCountAsync()
        {
            return await Task.FromResult(Products.Count);
        }

        #endregion
    }
}
