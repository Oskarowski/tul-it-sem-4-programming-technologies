using Presentation.Model.API;
using Presentation.ViewModel;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    internal class UserDetailViewModel : IViewModel, IUserDetailViewModel
    {
        public ICommand UpdateUser { get; set; }

        private readonly IUserModelOperation _modelOperation;
        private readonly IErrorInformer _informer;

        private string? _guid;
        private string? _firstName;
        private string? _lastName;
        private string? _email;
        private double _balance;
        private string? _phoneNumber;

        public string Guid
        {
            get => _guid;
            set
            {
                _guid = value;
                OnPropertyChanged(nameof(Guid));
            }
        }


        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }


        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        } 
        
        public double Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        } 
        
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public UserDetailViewModel(IUserModelOperation? model = null, IErrorInformer? informer = null)
        {
            this.UpdateUser = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = model ?? IUserModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();
        }

        public UserDetailViewModel(string guid, string firstName, string lastName, string email, double balance, string phoneNumber, IUserModelOperation? model = null, IErrorInformer? informer = null)
        {
            this.Guid = guid;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Balance = balance;
            this.PhoneNumber= phoneNumber;

            this.UpdateUser = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = model ?? IUserModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();
        }

        private void Update()
        {
            Task.Run(() =>
            {
                this._modelOperation.UpdateAsync(this.Guid, this.FirstName, this.LastName, this.Email, this.Balance, this.PhoneNumber);

                this._informer.InformSuccess("User successfully updated!");
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(this.FirstName) ||
                string.IsNullOrWhiteSpace(this.LastName) ||
                string.IsNullOrWhiteSpace(this.Email) ||
                string.IsNullOrWhiteSpace(this.PhoneNumber) ||
                this.Balance == 0
            );
        }
    }

}
