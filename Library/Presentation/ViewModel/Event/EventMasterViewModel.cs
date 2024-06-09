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
    internal class EventMasterViewModel : IViewModel, IEventMasterViewModel
    {
        public ICommand SwitchToUserMasterPage { get; set; }

        public ICommand SwitchToProductMasterPage { get; set; }

        public ICommand SwitchToStateMasterPage { get; set; }

        public ICommand PurchaseEvent { get; set; }

        public ICommand ReturnEvent { get; set; }

        public ICommand SupplyEvent { get; set; }

        public ICommand RemoveEvent { get; set; }

        private readonly IEventModelOperation _modelOperation;

        private ObservableCollection<IEventDetailViewModel> _events;

        public ObservableCollection<IEventDetailViewModel> Events
        {
            get => _events;
            set
            {
                _events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        private string _stateGuid;
        private string _userGuid;

        public string StateGuid
        {
            get => _stateGuid;
            set
            {
                _stateGuid = value;
                OnPropertyChanged(nameof(StateGuid));
            }
        }

        public string UserGuid
        {
            get => _userGuid;
            set
            {
                _userGuid = value;
                OnPropertyChanged(nameof(UserGuid));
            }
        }

        private bool _isEventSelected;

        public bool IsEventSelected
        {
            get => _isEventSelected;
            set
            {
                this.IsEventDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

                _isEventSelected = value;
                OnPropertyChanged(nameof(IsEventSelected));
            }
        }

        private Visibility _isEventDetailVisible;

        public Visibility IsEventDetailVisible
        {
            get => _isEventDetailVisible;
            set
            {
                _isEventDetailVisible = value;
                OnPropertyChanged(nameof(IsEventDetailVisible));
            }
        }

        private IEventDetailViewModel _selectedDetailViewModel;

        public IEventDetailViewModel SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                this.IsEventSelected = true;

                OnPropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public EventMasterViewModel(IEventModelOperation? model = null)
        {
            this.SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
            this.SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
            this.SwitchToProductMasterPage = new SwitchViewCommand("ProductMasterView");

            this.PurchaseEvent = new OnClickCommand(e => this.StorePurchaseEvent(), c => this.CanPurchaseEvent());
            this.ReturnEvent = new OnClickCommand(e => this.StoreReturnEvent(), c => this.CanReturnEvent());
            this.SupplyEvent = new OnClickCommand(e => this.StoreSupplyEvent(), c => this.CanSupplyEvent());
            this.RemoveEvent = new OnClickCommand(e => this.DeleteEvent());

            this.Events = new ObservableCollection<IEventDetailViewModel>();

            this._modelOperation = IEventModelOperation.CreateModelOperation();

            this.IsEventSelected = false;

            Task.Run(this.LoadEvents);
        }

        private bool CanPurchaseEvent()
        {
            return !(
                string.IsNullOrWhiteSpace(this.StateGuid) ||
                string.IsNullOrWhiteSpace(this.UserGuid)
            );
        }

        private bool CanReturnEvent()
        {
            return !(
                string.IsNullOrWhiteSpace(this.StateGuid) ||
                string.IsNullOrWhiteSpace(this.UserGuid)
            );
        }

        private bool CanSupplyEvent()
        {
            return !(
                string.IsNullOrWhiteSpace(this.StateGuid) ||
                string.IsNullOrWhiteSpace(this.UserGuid)
            );
        }

        private void StorePurchaseEvent()
        {
            Task.Run(async () =>
            {
                try
                {
                    string guid = Guid.NewGuid().ToString();

                    await this._modelOperation.AddAsync(guid, this.StateGuid, this.UserGuid, DateTime.Now, "RentEvent");

                    this.LoadEvents();
                }
                catch (Exception e)
                {

                }
            });
        }

        private void StoreReturnEvent()
        {
            Task.Run(async () =>
            {
                string guid = Guid.NewGuid().ToString();

                await this._modelOperation.AddAsync(guid, this.StateGuid, this.UserGuid, DateTime.Now, "ReturnEvent");

                this.LoadEvents();
            });
        }

        private void StoreSupplyEvent()
        {
            Task.Run(async () =>
            {
                string guid = Guid.NewGuid().ToString();

                await this._modelOperation.AddAsync(guid, this.StateGuid, this.UserGuid, DateTime.Now, "SupplyEvent");

                this.LoadEvents();
            });
        }

        private void DeleteEvent()
        {
            Task.Run(async () =>
            {
                await this._modelOperation.DeleteAsync(this.SelectedDetailViewModel.Guid);

                this.LoadEvents();
            });
        }

        private async void LoadEvents()
        {
            Dictionary<string, IEventModel> Events = (await this._modelOperation.GetAllAsync());

            Application.Current.Dispatcher.Invoke(() =>
            {
                this._events.Clear();

                foreach (IEventModel e in Events.Values)
                {
                    this._events.Add(new EventDetailViewModel(e.Guid, e.StateGuid, e.UserGuid, e.CreatedAt, e.Type));
                }
            });

            OnPropertyChanged(nameof(Events));
        }

    }
}
