using Presentation.Model.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Presentation;
using Presentation.ViewModel;

namespace Presentationion.ViewModel
{
    public interface IUserMasterViewModel
    {
        static IUserMasterViewModel CreateViewModel(IUserModelOperation operation, IErrorInformer informer)
        {
            return new UserMasterViewModel(operation, informer);
        }

        ICommand CreateUser { get; set; }

        ICommand RemoveUser { get; set; }

        ObservableCollection<IUserDetailViewModel> Users { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        double Balance { get; set; }

        bool IsUserSelected { get; set; }

        Visibility IsUserDetailVisible { get; set; }

        IUserDetailViewModel SelectedDetailViewModel { get; set; }
    }
}
