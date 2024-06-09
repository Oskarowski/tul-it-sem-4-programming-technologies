using Presentation.Model.API;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System;

namespace Presentation.ViewModel
{
    internal class StateMasterViewModel : IViewModel, IStateMasterViewModel
    {
        public ICommand SwitchToUserMasterPage { get; set; }

        public ICommand SwitchToProductMasterPage { get; set; }

        public ICommand SwitchToEventMasterPage { get; set; }

        public ICommand CreateState { get; set; }

        public ICommand RemoveState { get; set; }

        private readonly IStateModelOperation _modelOperation;

        private ObservableCollection<IStateDetailViewModel> _states;

        public ObservableCollection<IStateDetailViewModel> States
        {
            get => _states;
            set
            {
                _states = value;
                OnPropertyChanged(nameof(States));
            }
        }

        private string _guid;
        private string _productGuid;
        private int _quantity;

        public string Guid
        {
            get => _guid;
            set
            {
                _guid = value;
                OnPropertyChanged(nameof(Guid));
            }
        }

        public string ProductGuid
        {
            get => _productGuid;
            set
            {
                _productGuid = value;
                OnPropertyChanged(nameof(ProductGuid));
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private bool _isStateSelected;

        public bool IsStateSelected
        {
            get => _isStateSelected;
            set
            {
                this.IsStateDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

                _isStateSelected = value;
                OnPropertyChanged(nameof(IsStateSelected));
            }
        }

        private Visibility _isStateDetailVisible;

        public Visibility IsStateDetailVisible
        {
            get => _isStateDetailVisible;
            set
            {
                _isStateDetailVisible = value;
                OnPropertyChanged(nameof(IsStateDetailVisible));
            }
        }

        private IStateDetailViewModel _selectedDetailViewModel;

        public IStateDetailViewModel SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                this.IsStateSelected = true;

                OnPropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public StateMasterViewModel(IStateModelOperation? model = null)
        {
            this.SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
            this.SwitchToProductMasterPage = new SwitchViewCommand("ProductMasterView");
            this.SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

            this.CreateState = new OnClickCommand(e => this.StoreState(), c => this.CanStoreState());
            this.RemoveState = new OnClickCommand(e => this.DeleteState());

            this.States = new ObservableCollection<IStateDetailViewModel>();

            this._modelOperation = IStateModelOperation.CreateModelOperation();

            this.IsStateSelected = false;

            Task.Run(this.LoadStates);
        }

        private bool CanStoreState()
        {
            return !(
                string.IsNullOrWhiteSpace(this.ProductGuid) ||
                string.IsNullOrWhiteSpace(this.Quantity.ToString()) ||
                this.Quantity < 0
            );
        }

        private void StoreState()
        {
            Task.Run(async () =>
            {
                try
                {
                    string guid = System.Guid.NewGuid().ToString();

                    await this._modelOperation.AddAsync(guid, this.ProductGuid, this.Quantity);

                    this.LoadStates();
                }
                catch (Exception e)
                {

                }
            });
        }

        private void DeleteState()
        {
            Task.Run(async () =>
            {
                try
                {
                    await this._modelOperation.DeleteAsync(this.SelectedDetailViewModel.Guid);

                    this.LoadStates();
                }
                catch (Exception e)
                {

                }
            });
        }

        private async void LoadStates()
        {
            Dictionary<string, IStateModel> States = await this._modelOperation.GetAllAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                this._states.Clear();

                foreach (IStateModel s in States.Values)
                {
                    this._states.Add(new StateDetailViewModel(s.Guid, s.ProductGuid, s.Quantity));
                }
            });

            OnPropertyChanged(nameof(States));
        }
    }
}
