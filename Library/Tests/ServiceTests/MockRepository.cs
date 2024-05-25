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
        public Dictionary<string, IProduct> Products { get; set; } = new Dictionary<string, IProduct>();

        #region User CRUD

        public async Task AddUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            this.Users.Add(guid, new MockUser(guid, firstName, lastName, email, balance, phoneNumber));
        }

        public async Task<IUser> GetUserAsync(string guid)
        {
            return await Task.FromResult(this.Users[guid]);
        }

        public async Task UpdateUserAsync(string guid, string firstName, string lastName, string email, double balance, string phoneNumber)
        {
            this.Users[guid].FirstName = firstName;
            this.Users[guid].LastName = lastName;
            this.Users[guid].Email = email;
            this.Users[guid].Balance = balance;
            this.Users[guid].PhoneNumber = phoneNumber;
        }

        public async Task DeleteUserAsync(string guid)
        {
            this.Users.Remove(guid);
        }

        public async Task<Dictionary<string, IUser>> GetAllUsersAsync()
        {
            return await Task.FromResult(this.Users);
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await Task.FromResult(this.Users.Count);
        }

        #endregion


        #region Event CRUD

        public async Task AddEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            this.Events.Add(guid, new MockEvent(guid, userGuid, stateGuid, type, createdAt));
        }

        public async Task<IEvent> GetEventAsync(string guid)
        {
            return await Task.FromResult(this.Events[guid]);
        }

        public async Task UpdateEventAsync(string guid, string stateGuid, string userGuid, DateTime createdAt, string type)
        {
            this.Events[guid].StateGuid = stateGuid;
            this.Events[guid].UserGuid = userGuid;
            this.Events[guid].CreatedAt = createdAt;
            this.Events[guid].Type = type;
        }

        public async Task DeleteEventAsync(string guid)
        {
            this.Users.Remove(guid);
        }

        public async Task<Dictionary<string, IEvent>> GetAllEventsAsync()
        {
            return await Task.FromResult(this.Events);
        }

        public async Task<int> GetEventsCountAsync()
        {
            return await Task.FromResult(this.Events.Count);
        }

        #endregion


        #region State CRUD

        public async Task AddStateAsync(string guid, string productGuid, int quantity)
        {
            this.States.Add(guid, new MockState(guid, quantity, productGuid));
        }

        public async Task<IState> GetStateAsync(string guid)
        {
            return await Task.FromResult(this.States[guid]);
        }

        public async Task UpdateStateAsync(string guid, string productGuid, int quantity)
        {
            this.States[guid].ProductGuid = productGuid;
            this.States[guid].Quantity = quantity;
        }

        public async Task DeleteStateAsync(string guid)
        {
            this.States.Remove(guid);
        }

        public async Task<Dictionary<string, IState>> GetAllStatesAsync()
        {
            return await Task.FromResult(this.States);
        }

        public async Task<int> GetStatesCountAsync()
        {
            return await Task.FromResult(this.States.Count);
        }

        #endregion


        #region Product CRUD

        public async Task AddProductAsync(string guid, string name, double price)
        {
            this.Products.Add(guid, new MockProduct(guid, name, price));
        }

        public async Task<IProduct> GetProductAsync(string guid)
        {
            return await Task.FromResult(this.Products[guid]);
        }

        public async Task UpdateProductAsync(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate)
        {
            this.Products[guid].Name = name;
            this.Products[guid].Price = price;
        }

        public async Task DeleteProductAsync(string guid)
        {
            this.Products.Remove(guid);
        }

        public async Task<Dictionary<string, IProduct>> GetAllProductsAsync()
        {   
            return await Task.FromResult(this.Products);
        }

        public async Task<int> GetProductsCountAsync()
        {
            return await Task.FromResult(this.Products.Count);
        }

        #endregion
    }
}
