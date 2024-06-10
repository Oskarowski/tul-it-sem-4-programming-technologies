using Presentation.Model.API;
using Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    internal class EventDetailViewModel : IViewModel, IEventDetailViewModel
    {
        public ICommand UpdateEvent { get; set; }

        private readonly IEventModelOperation _modelOperation;

        private string _guid;
        private string _stateGuid;
        private string _userGuid;
        private string _type;
        private DateTime _createdAt;

        public string Guid
        {
            get => _guid;
            set
            {
                _guid = value;
                OnPropertyChanged(nameof(Guid));
            }
        }

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

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set
            {
                _createdAt = value;
                OnPropertyChanged(nameof(CreatedAt));
            }
        }

        private DateTime _occurrenceDate;

        public DateTime OccurrenceDate
        {
            get => _occurrenceDate;
            set
            {
                _occurrenceDate = value;
                OnPropertyChanged(nameof(OccurrenceDate));
            }
        }

        public EventDetailViewModel(IEventModelOperation? model = null)
        {
            this.UpdateEvent = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = IEventModelOperation.CreateModelOperation();
        }

        public EventDetailViewModel(string guid, string stateGuid, string userGuid, DateTime createdAt, string type, IEventModelOperation? model = null)
        {
            this.UpdateEvent = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = IEventModelOperation.CreateModelOperation();

            this.Guid = guid;
            this.StateGuid = stateGuid;
            this.UserGuid = userGuid;
            this.CreatedAt = createdAt;
            this.Type = type;
        }

        private void Update()
        {
            Task.Run(async () =>
            {
                await this._modelOperation.UpdateAsync(this.Guid, this.StateGuid, this.UserGuid, this.OccurrenceDate, this.Type);
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(this.OccurrenceDate.ToString())
            );
        }
    }
}
