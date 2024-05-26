using Presentation.Model.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    public interface IUserDetailViewModel
    {
        static IUserDetailViewModel CreateViewModel(string guid, string firstName, string lastName, string email, double balance, string phoneNumber, IUserModelOperation model, IErrorInformer informer)
        {
            return new UserDetailViewModel(guid, firstName, lastName, email, balance, phoneNumber, model, informer);
        }

        string Guid { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        double Balance { get; set; }
        string PhoneNumber { get; set; }
    }
}
