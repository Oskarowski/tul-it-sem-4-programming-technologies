﻿using Presentation.Model.API;
using System.Windows.Input;
using System.ComponentModel;

namespace Presentation.ViewModel 
{
    internal class StateDetailViewModel : IViewModel, IStateDetailViewModel
    {
        public ICommand UpdateState { get; set; }

        private readonly IStateModelOperation _modelOperation;

        private readonly IErrorInformer _informer;

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

        public StateDetailViewModel(IStateModelOperation? model = null, IErrorInformer? informer = null)
        {
            this.UpdateState = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = IStateModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();
        }

        public StateDetailViewModel(string guid, string productGuid, int quantity, IStateModelOperation? model = null, IErrorInformer? informer = null)
        {
            this.Guid = guid;
            this.ProductGuid = productGuid;
            this.Quantity = quantity;

            this.UpdateState = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = IStateModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();
        }

        private void Update()
        {
            Task.Run(() =>
            {
                this._modelOperation.UpdateAsync(this.Guid, this.ProductGuid, this.Quantity);

                this._informer.InformSuccess("State successfully updated!");
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(this.Quantity.ToString()) ||
                this.Quantity < 0
            );
        }
    }
}