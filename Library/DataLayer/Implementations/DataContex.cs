using DataLayer.API;
using DataLayer.Database;
using System;

namespace DataLayer.Implementations
{
    public class DataContext : IDataContext
    {
        public static IDataContext NewInstance(string? connectionString = null)
        {
            return new DataContext(connectionString);
        }

        private readonly string _connectionString;

        public DataContext(string? connectionString = null)
        {
            if (connectionString is null)
            {
                string _projectRootDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
                string _DBRelativePath = @"DataLayer\Database\Database.mdf";
                string _DBPath = Path.Combine(_projectRootDir, _DBRelativePath);
                this._connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
            }
            else
            {
                this._connectionString = connectionString;
            }
        }

        #region User CRUD

        public async Task AddUserAsync(IUser user)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.User entity = new Database.User()
                {
                    guid = user.Guid,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email,
                    balance = user.Balance,
                    phoneNumber = user.PhoneNumber
                };

                context.User.InsertOnSubmit(entity);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<IUser?> GetUserAsync(string guid)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.User? user = await Task.Run(() =>
                {
                    IQueryable<Database.User> query = context.User.Where(u => u.guid == guid);

                    return query.FirstOrDefault();
                });
                
                return user is not null ? new User(user.guid, user.firstName, user.lastName, user.email, user.balance, user.phoneNumber) : null;
            }
        }

        public async Task UpdateUserAsync(IUser user)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.User toUpdate = (from u in context.User where u.guid == user.Guid select u).FirstOrDefault()!;

                toUpdate.firstName = user.FirstName;
                toUpdate.lastName = user.LastName;
                toUpdate.email = user.Email;
                toUpdate.balance = user.Balance;
                toUpdate.phoneNumber = user.PhoneNumber;

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task DeleteUserAsync(string guid)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.User toDelete = (from u in context.User where u.guid == guid select u).FirstOrDefault()!;

                context.User.DeleteOnSubmit(toDelete);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<Dictionary<string, IUser>> GetAllUsersAsync()
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                IQueryable<IUser> usersQuery = from u in context.User
                                               select
                                                   new User(u.guid, u.firstName, u.lastName, u.email, u.balance, u.phoneNumber) as IUser;

                return await Task.Run(() => usersQuery.ToDictionary(k => k.Guid));
            }
        }

        public async Task<int> GetUsersCountAsync()
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                return await Task.Run(() => context.User.Count());
            }
        }

        #endregion

        #region Product CRUD

        public async Task AddProductAsync(IBook product)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.Book entity = new Database.Book()
                {
                    guid = product.Guid,
                    name = product.Name,
                    price = product.Price,

                    author = (product as IBook)?.Author,
                    publisher = (product as IBook)?.Publisher,
                    pages = (int)(product as IBook)?.Pages,
                    publicationDate = (DateTime)(product as IBook)?.PublicationDate
                };

                context.Book.InsertOnSubmit(entity);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<IBook?> GetProductAsync(string guid)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.Book? product = await Task.Run(() =>
                {
                    IQueryable<Database.Book> query =
                        from p in context.Book
                        where p.guid == guid
                        select p;

                    return query.FirstOrDefault();
                });

                return product is not null ? new Book(product.guid, product.name, (double)product.price, product.author, product.publisher, product.pages, product.publicationDate) : null;
            }
        }

        public async Task UpdateProductAsync(IBook product)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.Book toUpdate = (from p in context.Book where p.guid == product.Guid select p).FirstOrDefault()!;

                toUpdate.name = product.Name;
                toUpdate.price = product.Price;
                
                toUpdate.author = (product as IBook)?.Author;
                toUpdate.publisher = (product as IBook)?.Publisher;
                toUpdate.pages = (int)(product as IBook)?.Pages;
                toUpdate.publicationDate = (DateTime)(product as IBook)?.PublicationDate;


                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task DeleteProductAsync(string guid)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.Book toDelete = (from p in context.Book where p.guid == guid select p).FirstOrDefault()!;

                context.Book.DeleteOnSubmit(toDelete);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<Dictionary<string, IBook>> GetAllProductsAsync()
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                IQueryable<IBook> productQuery = from p in context.Book
                                                    select
                                                        new Book(p.guid, p.name, (double)p.price, p.author, p.publisher, p.pages, p.publicationDate) as IBook;

                return await Task.Run(() => productQuery.ToDictionary(k => k.Guid));
            }
        }

        public async Task<int> GetProductsCountAsync()
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                return await Task.Run(() => context.Book.Count());
            }
        }

        #endregion


        #region State CRUD

        public async Task AddStateAsync(IState state)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.State entity = new Database.State()
                {
                    guid = state.Guid,
                    productGuid = state.ProductGuid,
                    quantity = state.Quantity
                };

                context.State.InsertOnSubmit(entity);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<IState?> GetStateAsync(string guid)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.State? state = await Task.Run(() =>
                {
                    IQueryable<Database.State> query =
                        from s in context.State
                        where s.guid == guid
                        select s;

                    return query.FirstOrDefault();
                });

                return state is not null ? new State(state.guid, state.productGuid, state.quantity) : null;
            }
        }

        public async Task UpdateStateAsync(IState state)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.State toUpdate = (from s in context.State where s.guid == state.Guid select s).FirstOrDefault()!;

                toUpdate.productGuid = state.ProductGuid;
                toUpdate.quantity = state.Quantity;

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task DeleteStateAsync(string guid)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.State toDelete = (from s in context.State where s.guid == guid select s).FirstOrDefault()!;

                context.State.DeleteOnSubmit(toDelete);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<Dictionary<string, IState>> GetAllStatesAsync()
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                IQueryable<IState> stateQuery = from s in context.State
                                                select
                                                    new State(s.guid, s.productGuid, s.quantity) as IState;

                return await Task.Run(() => stateQuery.ToDictionary(k => k.Guid));
            }
        }

        public async Task<int> GetStatesCountAsync()
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                return await Task.Run(() => context.State.Count());
            }
        }

        #endregion


        #region Event CRUD

        public async Task AddEventAsync(IEvent even)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.Event entity = new Database.Event()
                {
                    guid = even.Guid,
                    stateGuid = even.StateGuid,
                    userGuid = even.UserGuid,
                    createdAt = even.CreatedAt,
                    type = even.Type,
                };

                context.Event.InsertOnSubmit(entity);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<IEvent?> GetEventAsync(string guid)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.Event? even = await Task.Run(() =>
                {
                    IQueryable<Database.Event> query =
                        from e in context.Event
                        where e.guid == guid
                        select e;

                    return query.FirstOrDefault();
                });

                return even is not null ? new Event(even.guid, even.stateGuid, even.userGuid, even.createdAt, even.type) : null;
            }

        }

        public async Task UpdateEventAsync(IEvent even)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.Event toUpdate = (from e in context.Event where e.guid == even.Guid select e).FirstOrDefault()!;

                toUpdate.stateGuid = even.StateGuid;
                toUpdate.userGuid = even.UserGuid;
                toUpdate.createdAt = even.CreatedAt;
                toUpdate.type = even.Type;

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task DeleteEventAsync(string guid)
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                Database.Event toDelete = (from e in context.Event where e.guid == guid select e).FirstOrDefault()!;

                context.Event.DeleteOnSubmit(toDelete);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<Dictionary<string, IEvent>> GetAllEventsAsync()
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                IQueryable<IEvent> eventQuery = from e in context.Event
                                                select
                                                    new Event(e.guid, e.stateGuid, e.userGuid, e.createdAt, e.type) as IEvent;

                return await Task.Run(() => eventQuery.ToDictionary(k => k.Guid));
            }
        }

        public async Task<int> GetEventsCountAsync()
        {
            using (LibraryDataClassesDataContext context = new LibraryDataClassesDataContext(_connectionString))
            {
                return await Task.Run(() => context.Event.Count());
            }
        }

        #endregion


        #region Helpers

        public async Task<bool> CheckIfUserExists(string guid)
        {
            return (await GetUserAsync(guid)) != null;
        }

        public async Task<bool> CheckIfProductExists(string guid)
        {
            return (await GetProductAsync(guid)) != null;
        }

        public async Task<bool> CheckIfStateExists(string guid)
        {
            return (await GetStateAsync(guid)) != null;
        }

        public async Task<bool> CheckIfEventExists(string guid, string type)
        {
            return (await GetEventAsync(guid)) != null;
        }

        #endregion

    }
}