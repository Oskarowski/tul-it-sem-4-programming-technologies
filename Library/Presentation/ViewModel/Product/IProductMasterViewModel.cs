using Presentation.Model.API;
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
    public interface IProductMasterViewModel
    {
        static IProductMasterViewModel CreateViewModel(IProductModelOperation operation)
        {
            return new ProductMasterViewModel(operation);
        }

        ICommand CreateProduct { get; set; }

        ICommand RemoveProduct { get; set; }

        Visibility IsProductDetailVisible { get; set; }

        IProductDetailViewModel SelectedDetailViewModel { get; set; }
        
        ObservableCollection<IProductDetailViewModel> Products { get; set; }

        string Name { get; set; }
        double Price { get; set; }
        string Author { get; set; }
        string Publisher { get; set; }
        int Pages { get; set; }
        DateTime PublicationDate { get; set; }
    }
}
