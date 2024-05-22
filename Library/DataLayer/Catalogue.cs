using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace DataLayer
{
    partial class CatalogueDataContext
    {
        #region Extensibility Method Definitions
        partial void DeleteProduct(Product instance) 
        {
            this.Products.DeleteOnSubmit(instance);
            this.SubmitChanges();
        }

        partial void DeleteUser(User instance)
        {
            this.Users.DeleteOnSubmit(instance);
            this.SubmitChanges();
        }

        partial void DeleteState(State instance)
        {
            this.States.DeleteOnSubmit(instance);
            this.SubmitChanges();
        }

        partial void DeleteEvent(Event instance)
        {
            this.Events.DeleteOnSubmit(instance);
            this.SubmitChanges();
        }

        partial void DeleteBook(Book instance)
        {
            // TODO: Implement this method
            this.Books.DeleteOnSubmit(instance);
            this.SubmitChanges();
        }

        partial void InsertProduct(Product instance)
        {
            this.Products.InsertOnSubmit(instance);
            this.SubmitChanges();
        }

        partial void InsertUser(User instance)
        {
            this.Users.InsertOnSubmit(instance);
            this.SubmitChanges();
        }

        partial void InsertState(State instance)
        {
            this.States.InsertOnSubmit(instance);
            this.SubmitChanges();
        }

        partial void InsertEvent(Event instance)
        {
            this.Events.InsertOnSubmit(instance);
            this.SubmitChanges();
        }

        partial void InsertBook(Book instance)
        {
            this.Books.InsertOnSubmit(instance);
            this.SubmitChanges();
        }
        #endregion

        #region Delete Methods
        public void DeleteUserById(Guid ID)
        {
            User user = this.Users.SingleOrDefault(u => u.ID == ID);
            if (user != null)
            {
                Event eventInstance = this.Events.SingleOrDefault(e => e.UserID == ID);
                if (eventInstance != null)
                {
                    eventInstance.UserID = null;
                }

                this.Users.DeleteOnSubmit(user);
                this.SubmitChanges();
            }
        }

        public void DeleteBookById(Guid ID)
        {
            Book book = this.Books.SingleOrDefault(b => b.ProductID == ID);
            if (book != null)
            {
                // delete the associated product state
                State state = this.States.SingleOrDefault(s => s.ProductID == ID);
                if (state != null)
                {
                    this.States.DeleteOnSubmit(state);
                }

                // Retrieve the associated product instance to delete
                Product product = this.Products.SingleOrDefault(p => p.ID == ID);

                // Check if the associated product exists and delete it
                if (product != null)
                {
                    this.Products.DeleteOnSubmit(product);
                }

                // Delete the book
                this.Books.DeleteOnSubmit(book);

                // Submit the changes to the database
                this.SubmitChanges();
            }
        }

        public void DeleteEventById(Guid ID)
        {
            Event eventInstance = this.Events.SingleOrDefault(e => e.ID == ID);
            if (eventInstance != null)
            {
                State state = this.States.SingleOrDefault(s => s.ID == eventInstance.StateID);
                if (eventInstance.EventType == "Borrow")
                {
                    state.Quantity++;
                }
                else if (eventInstance.EventType == "Return")
                {
                    state.Quantity--;
                }
                else if (eventInstance.EventType == "Delivery")
                {
                    state.Quantity -= eventInstance.Amount.Value;
                }

                this.Events.DeleteOnSubmit(eventInstance);
                this.SubmitChanges();
            }
        }

        public void DeleteStateById(Guid ID)
        {
            State state = this.States.SingleOrDefault(s => s.ID == ID);
            if (state != null)
            {
                Event eventInstance = this.Events.SingleOrDefault(e => e.StateID == ID);
                if (eventInstance != null)
                {
                    eventInstance.StateID = null;
                }

                this.States.DeleteOnSubmit(state);
                this.SubmitChanges();
            }
        }

        public void DeleteUsersBy(Guid? ID = null, string firstName = null,
                          string lastName = null, string email = null,
                          string phoneNumber = null)
        {
            var usersToDelete = this.Users.AsQueryable();

            if (ID.HasValue)
            {
                usersToDelete = usersToDelete.Where(u => u.ID == ID.Value);
            }
            if (!string.IsNullOrEmpty(firstName))
            {
                usersToDelete = usersToDelete.Where(u => u.FirstName == firstName);
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                usersToDelete = usersToDelete.Where(u => u.LastName == lastName);
            }
            if (!string.IsNullOrEmpty(email))
            {
                usersToDelete = usersToDelete.Where(u => u.Email == email);
            }
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                usersToDelete = usersToDelete.Where(u => u.PhoneNumber == phoneNumber);
            }

            var userIdsToDelete = usersToDelete.Select(u => u.ID).ToList();

            if (userIdsToDelete.Any())
            {
                // Update associated Events to nullify UserID
                var eventsToUpdate = this.Events.Where(e => userIdsToDelete.Contains(e.UserID.Value));
                foreach (var eventInstance in eventsToUpdate)
                {
                    eventInstance.UserID = null;
                }

                // Delete the users
                this.Users.DeleteAllOnSubmit(usersToDelete);
                this.SubmitChanges();
            }
        }

        public void DeleteBooksBy(Guid? ID = null, string name = null, decimal? price = null,
                          string author = null, string publisher = null,
                          int? pages = null, DateTime? publicationDate = null)
        {
            var booksToDelete = this.Books.AsQueryable();

            if (ID.HasValue)
            {
                booksToDelete = booksToDelete.Where(b => b.ProductID == ID.Value);
            }
            if (!string.IsNullOrEmpty(name))
            {
                booksToDelete = booksToDelete.Where(b => b.Product.Name == name);
            }
            if (price.HasValue)
            {
                booksToDelete = booksToDelete.Where(b => b.Product.Price == price.Value);
            }
            if (!string.IsNullOrEmpty(author))
            {
                booksToDelete = booksToDelete.Where(b => b.Author == author);
            }
            if (!string.IsNullOrEmpty(publisher))
            {
                booksToDelete = booksToDelete.Where(b => b.Publisher == publisher);
            }
            if (pages.HasValue)
            {
                booksToDelete = booksToDelete.Where(b => b.Pages == pages.Value);
            }
            if (publicationDate.HasValue)
            {
                booksToDelete = booksToDelete.Where(b => b.PublicationDate == publicationDate.Value);
            }

            var bookIdsToDelete = booksToDelete.Select(b => b.ProductID).ToList();

            if (bookIdsToDelete.Any())
            {
                // Update associated States to nullify ProductID
                var statesToUpdate = this.States.Where(s => bookIdsToDelete.Contains(s.ProductID));
                foreach (var state in statesToUpdate)
                {
                    var eventsToUpdate = this.Events.Where(e => e.StateID == state.ID);
                    foreach (var eventInstance in eventsToUpdate)
                    {
                        eventInstance.StateID = null;
                    }
                    this.States.DeleteOnSubmit(state);
                }

                // delete products associated with the books
                var productIdsToDelete = booksToDelete.Select(b => b.ProductID).ToList();
                foreach (var productID in productIdsToDelete)
                {
                    var product = this.Products.SingleOrDefault(p => p.ID == productID);
                    if (product != null)
                    {
                        this.Products.DeleteOnSubmit(product);
                    }
                }

                // Delete the books
                this.Books.DeleteAllOnSubmit(booksToDelete);
                this.SubmitChanges();
            }
        }

        public void DeleteProductsBy(Guid? ID = null, string name = null, decimal? price = null)
        {
            var roductsToDelete = this.Products.AsQueryable();

            if (ID.HasValue)
            {
                roductsToDelete = roductsToDelete.Where(p => p.ID == ID.Value);
            }
            if (!string.IsNullOrEmpty(name))
            {
                roductsToDelete = roductsToDelete.Where(p => p.Name == name);
            }
            if (price.HasValue)
            {
                roductsToDelete = roductsToDelete.Where(p => p.Price == price.Value);
            }

            var productIdsToDelete = roductsToDelete.Select(p => p.ID).ToList();

            if (productIdsToDelete.Any())
            {
                // Update associated States to nullify ProductID
                var statesToUpdate = this.States.Where(s => productIdsToDelete.Contains(s.ProductID));
                foreach (var state in statesToUpdate)
                {
                    var eventsToUpdate = this.Events.Where(e => e.StateID == state.ID);
                    foreach (var eventInstance in eventsToUpdate)
                    {
                        eventInstance.StateID = null;
                    }
                    this.States.DeleteOnSubmit(state);
                }

                var bookIdsToDelete = this.Books.Where(b => productIdsToDelete.Contains(b.ProductID)).Select(b => b.ProductID).ToList();
                if (bookIdsToDelete.Any())
                {
                    // Delete the books
                    var booksToDelete = this.Books.Where(b => bookIdsToDelete.Contains(b.ProductID));
                    this.Books.DeleteAllOnSubmit(booksToDelete);
                }

                // Delete the products
                this.Products.DeleteAllOnSubmit(roductsToDelete);
                this.SubmitChanges();
            }
        }

        public void DeleteEventsBy(Guid? ID = null, Guid? userID = null, Guid? stateID = null,
                          string eventType = null, int? amount = null)
        {
            var eventsToDelete = this.Events.AsQueryable();

            if (ID.HasValue)
            {
                eventsToDelete = eventsToDelete.Where(e => e.ID == ID.Value);
            }
            if (userID.HasValue)
            {
                eventsToDelete = eventsToDelete.Where(e => e.UserID == userID.Value);
            }
            if (stateID.HasValue)
            {
                eventsToDelete = eventsToDelete.Where(e => e.StateID == stateID.Value);
            }
            if (!string.IsNullOrEmpty(eventType))
            {
                eventsToDelete = eventsToDelete.Where(e => e.EventType == eventType);
            }
            if (amount.HasValue)
            {
                eventsToDelete = eventsToDelete.Where(e => e.Amount == amount.Value);
            }

            var eventIdsToDelete = eventsToDelete.Select(e => e.ID).ToList();

            if (eventIdsToDelete.Any())
            {
                // Delete the events
                this.Events.DeleteAllOnSubmit(eventsToDelete);
                this.SubmitChanges();
            }
        }

        public void DeleteStatesBy(Guid? ID = null, Guid? productID = null, int? quantity = null)
        {
            var statesToDelete = this.States.AsQueryable();

            if (ID.HasValue)
            {
                statesToDelete = statesToDelete.Where(s => s.ID == ID.Value);
            }
            if (productID.HasValue)
            {
                statesToDelete = statesToDelete.Where(s => s.ProductID == productID.Value);
            }
            if (quantity.HasValue)
            {
                statesToDelete = statesToDelete.Where(s => s.Quantity == quantity.Value);
            }

            var stateIdsToDelete = statesToDelete.Select(s => s.ID).ToList();

            if (stateIdsToDelete.Any())
            {
                // Update associated Events to nullify StateID
                var eventsToUpdate = this.Events.Where(e => stateIdsToDelete.Contains(e.StateID.Value));
                foreach (var eventInstance in eventsToUpdate)
                {
                    eventInstance.StateID = null;
                }

                // Delete the states
                this.States.DeleteAllOnSubmit(statesToDelete);
                this.SubmitChanges();
            }
        }
        #endregion

        #region Insert Methods
        public void InsertUser(string firstName, string lastName, string email, string phoneNumber, Guid? ID = null)
        {
            User user = new User();
            user.ID = ID ?? Guid.NewGuid();
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.PhoneNumber = phoneNumber;
            user.Balance = 0;

            this.Users.InsertOnSubmit(user);
            this.SubmitChanges();
        }

        public void InsertBook(string name, decimal price, string author, 
                    string publisher, int pages, DateTime publicationDate, Guid? ID = null)
        {
            Product product = new Product();
            product.ID = ID ?? Guid.NewGuid();
            product.Name = name;
            product.Price = price;

            Book book = new Book();
            book.ProductID = product.ID;
            book.Author = author;
            book.Publisher = publisher;
            book.Pages = pages;
            book.PublicationDate = publicationDate;

            this.Products.InsertOnSubmit(product);
            this.Books.InsertOnSubmit(book);
            this.SubmitChanges();
        }

        public void InsertEvent(Guid userID, Guid stateID, string eventType, int? amount = null, Guid? ID = null)
        {
            Event @event = new Event();
            @event.ID = ID ?? Guid.NewGuid();
            @event.UserID = userID;
            @event.StateID = stateID;
            @event.EventType = eventType;

            State state = this.States.SingleOrDefault(s => s.ID == stateID);
            if (eventType == "Borrow") state.Quantity--;
            else if (eventType == "Return") state.Quantity++;
            else if (eventType == "Delivery") {
                if (amount <= 0)
                {
                    throw new Exception("Amount must be greater than 0");
                }
                @event.Amount = amount;
                state.Quantity += amount.Value;          
            }
            this.Events.InsertOnSubmit(@event);
            this.SubmitChanges();
        }

        public void InsertState(Guid productID, int quantity, Guid? ID = null)
        {
            State state = new State();
            state.ID = Guid.NewGuid();
            state.ProductID = productID;
            state.Quantity = quantity;

            this.States.InsertOnSubmit(state);
            this.SubmitChanges();
        }
        #endregion

        #region Update Methods
        public void UpdateBook(Guid ID, string name = null, decimal? price = null,
                    string author = null, string publisher = null,
                    int? pages = null, DateTime? publicationDate = null)
        {
            Product product = this.Products.SingleOrDefault(p => p.ID == ID);
            Book book = this.Books.SingleOrDefault(b => b.ProductID == ID);
            if (product != null)
            {
                if (book != null)
                {
                    if (author != null)
                    {
                        book.Author = author;
                    }
                    if (publisher != null)
                    {
                        book.Publisher = publisher;
                    }
                    if (pages.HasValue)
                    {
                        book.Pages = pages.Value;
                    }
                    if (publicationDate.HasValue)
                    {
                        book.PublicationDate = publicationDate.Value;
                    }
                }

                if (name != null)
                {
                    product.Name = name;
                }
                if (price.HasValue)
                {
                    product.Price = price.Value;
                }

                this.SubmitChanges();
            }
        }

        public void UpdateUser(Guid ID, string firstName = null, string lastName = null,
                    string email = null, string phoneNumber = null)
        {
            User user = this.Users.SingleOrDefault(u => u.ID == ID);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(firstName))
                {
                    user.FirstName = firstName;
                }
                if (!string.IsNullOrEmpty(lastName))
                {
                    user.LastName = lastName;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    user.Email = email;
                }
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    user.PhoneNumber = phoneNumber;
                }
                this.SubmitChanges();
            }
        }

        public void UpdateState(Guid ID, int quantity)
        {
            State state = this.States.SingleOrDefault(s => s.ID == ID);
            if (state != null)
            {
                state.Quantity = quantity;
                this.SubmitChanges();
            }
        }

        public void UpdateEvent(Guid ID, string eventType, int? amount = null)
        {
            Event @event = this.Events.SingleOrDefault(e => e.ID == ID);
            if (@event != null && @event.EventType != eventType)
            {
                string currentEventType = @event.EventType;
                State state = this.States.SingleOrDefault(s => s.ID == @event.StateID);
                if (eventType == "Borrow")
                {
                    if (currentEventType == "Delivery")
                    {
                        state.Quantity = state.Quantity - @event.Amount.Value - 1;
                    }
                    else if (currentEventType == "Return")
                    {
                        state.Quantity = state.Quantity + 2;
                    }
                }
                else if (eventType == "Return") 
                { 
                    if (currentEventType == "Delivery")
                    {
                        state.Quantity = state.Quantity - @event.Amount.Value + 1;
                    }
                    else if (currentEventType == "Borrow")
                    {
                        state.Quantity = state.Quantity - 2;
                    }
                }
                else if (eventType == "Delivery")
                {
                    if (currentEventType == "Borrow")
                    {
                        state.Quantity = state.Quantity + 1 + amount.Value;
                    }
                    else if (currentEventType == "Return")
                    {
                        state.Quantity = state.Quantity - 1 + amount.Value;
                    }
                    else if (amount <= 0)
                    {
                        throw new Exception("Amount must be greater than 0");
                    }
                }

                @event.EventType = eventType;
                this.SubmitChanges();
            }
        }
        #endregion

        #region Find Methods
        public IEnumerable<Product> FindProducts(string name = null, decimal? price = null)
        {
            return this.Products.Where(p => (name == null || p.Name == name) &&
                                                (!price.HasValue || p.Price == price.Value));
        }

        public IEnumerable<User> FindUsers(string firstName = null, string lastName = null,
                               string email = null, string phoneNumber = null)
        {
            return this.Users.Where(u => (firstName == null || u.FirstName == firstName) &&
                                            (lastName == null || u.LastName == lastName) &&
                                            (email == null || u.Email == email) &&
                                            (phoneNumber == null || u.PhoneNumber == phoneNumber));
        }

        public IEnumerable<State> FindStates(Guid? productID = null, int? quantity = null)
        {
            return this.States.Where(s => (!productID.HasValue || s.ProductID == productID.Value) &&
                                            (!quantity.HasValue || s.Quantity == quantity.Value));
        }

        public IEnumerable<Event> FindEvents(Guid? userID = null, Guid? stateID = null,
                                          string eventType = null, int? amount = null)
        {
            return this.Events.Where(e => (!userID.HasValue || e.UserID == userID.Value) &&
                                                (!stateID.HasValue || e.StateID == stateID.Value) &&
                                                (eventType == null || e.EventType == eventType) &&
                                                (!amount.HasValue || e.Amount == amount.Value));
        }

        public IEnumerable<Book> FindBooks(Guid? productID = null, string author = null,
                                                string publisher = null, int? pages = null,
                                                DateTime? publicationDate = null)
        {
            return this.Books.Where(b => (!productID.HasValue || b.ProductID == productID.Value) &&
                                                (author == null || b.Author == author) &&
                                                (publisher == null || b.Publisher == publisher) &&
                                                (!pages.HasValue || b.Pages == pages.Value) &&
                                                (!publicationDate.HasValue || b.PublicationDate == publicationDate.Value));
        }
        #endregion
    }
}