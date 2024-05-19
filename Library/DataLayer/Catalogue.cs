using System.Runtime.Remoting.Contexts;

namespace DataLayer
{
    partial class CatalogueDataContext
    {
        partial void DeleteProduct(Product instance) { 
            this.Products.DeleteOnSubmit(instance);
            this.SubmitChanges();
        }

        partial void DeleteUser(User instance)
        {
            this.Users.DeleteOnSubmit(instance);
            this.SubmitChanges();
        }

        partial void DeleteBook(Book instance)
        {
            this.Books.DeleteOnSubmit(instance);
            this.SubmitChanges();
        }

        partial void DeleteState(State instance)
        {
            this.States.DeleteOnSubmit(instance);
            this.SubmitChanges();
        }

        partial void DeleteEvent(Event instance)
        {
            var eventToRemove = this.Events.SingleOrDefault(e => e.ID == instance.ID);
            if (eventToRemove != null)
            {
                // Remove the event from the DataContext
                this.Events.DeleteOnSubmit(eventToRemove);

                // Submit changes to the database
                this.SubmitChanges();
            }
            else
            {
                // Handle case where event instance doesn't exist
                throw new ArgumentException("Event instance not found in database.", nameof(instance));
            }
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


    }
}