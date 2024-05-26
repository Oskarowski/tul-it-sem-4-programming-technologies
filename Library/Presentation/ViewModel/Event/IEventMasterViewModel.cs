using Presentation.Model.API;
using Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Presentation.ViewModel
{
    public interface IEventMasterViewModel
    {
        static IEventMasterViewModel CreateViewModel(IEventModelOperation operation, IErrorInformer informer)
        {
            return new EventMasterViewModel(operation, informer);
        }

        ICommand PurchaseEvent { get; set; }

        ICommand ReturnEvent { get; set; }

        ICommand SupplyEvent { get; set; }

        ICommand RemoveEvent { get; set; }

        ObservableCollection<IEventDetailViewModel> Events { get; set; }

        Visibility IsEventDetailVisible { get; set; }

        IEventDetailViewModel SelectedDetailViewModel { get; set; }

        bool IsEventSelected { get; set; }

        string StateGuid { get; set; }

        string UserGuid { get; set; }
    }
}
