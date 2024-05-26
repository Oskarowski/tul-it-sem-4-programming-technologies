using Presentation.Model.API;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace Presentation.ViewModel
{
    public interface IStateMasterViewModel
    {
        static IStateMasterViewModel CreateViewModel(IStateModelOperation operation, IErrorInformer informer)
        {
            return new StateMasterViewModel(operation, informer);
        }

        public ICommand CreateState { get; set; }

        public ICommand RemoveState { get; set; }

        public ObservableCollection<IStateDetailViewModel> States { get; set; }

        public Visibility IsStateDetailVisible { get; set; }

        public IStateDetailViewModel SelectedDetailViewModel { get; set; }

        public bool IsStateSelected { get; set; }

        public string Guid { get; set; }

        public string ProductGuid { get; set; }

        public int Quantity { get; set; }
    }
}
