using Presentation.Model.API;
using System.Windows.Input;
using System.Windows;
using System;

namespace Presentation.ViewModel
{
    public interface IStateDetailViewModel
    {
        static IStateDetailViewModel CreateViewModel(string guid, string productGuid, int quantity, IStateModelOperation model)
        {
            return new StateDetailViewModel(guid, productGuid, quantity, model);
        }

        ICommand UpdateState { get; set; }

        string Guid { get; set; }
        string ProductGuid { get; set; }
        int Quantity { get; set; }
    }
}
