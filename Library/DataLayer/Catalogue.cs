using System;
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
        
        public void DeleteProductById(Guid ID)
        {
            Product product = this.Products.SingleOrDefault(p => p.ID == ID);
            if (product != null)
            {
                // delete the associated product state
                State state = this.States.SingleOrDefault(s => s.ProductID == ID);
                if (state != null)
                {
                    this.States.DeleteOnSubmit(state);
                }

                // Retrieve the associated book instance to delete
                Book book = this.Books.SingleOrDefault(b => b.ProductID == ID);

                // Check if the associated book exists and delete it
                if (book != null)
                {
                    this.Books.DeleteOnSubmit(book);
                }

                // Delete the product
                this.Products.DeleteOnSubmit(product);

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
        #endregion

        #region Insert Methods
        public void InsertUser(string firstName, string lastName, string email, string phoneNumber)
        {
            User user = new User();
            user.ID = Guid.NewGuid();
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.PhoneNumber = phoneNumber;
            user.Balance = 0;

            this.Users.InsertOnSubmit(user);
            this.SubmitChanges();
        }

        public void InsertBook(string name, decimal price, string author, 
                    string publisher, int pages, DateTime publicationDate)
        {
            Product product = new Product();
            product.ID = Guid.NewGuid();
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

        public void InsertEvent(Guid userID, Guid stateID, string eventType, int amount = 0)
        {
            Event @event = new Event();
            @event.ID = Guid.NewGuid();
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
                state.Quantity += amount;          
            }
            this.Events.InsertOnSubmit(@event);
            this.SubmitChanges();
        }

        public void InsertState(Guid productID, int quantity)
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
    }
}