using Presentation.Model.API;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using Presentation.ViewModel;

namespace Presentation.ViewModel
{
    public class UserMasterViewModel : IViewModel, IUserMasterViewModel
    {
        public ICommand SwitchToProductMasterPage { get; set; }

        public ICommand SwitchToStateMasterPage { get; set; }

        public ICommand SwitchToEventMasterPage { get; set; }

        public ICommand CreateUser { get; set; }

        public ICommand RemoveUser { get; set; }

        private readonly IUserModelOperation _modelOperation;

        private ObservableCollection<IUserDetailViewModel> _users;

        public ObservableCollection<IUserDetailViewModel> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private string _firstName;
        private string _lastName;
        private string _email;
        private double _balance;
        private string _phoneNumber;

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

        private bool _isUserSelected;

        public bool IsUserSelected
        {
            get => _isUserSelected;
            set
            {
                this.IsUserDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

                _isUserSelected = value;
                OnPropertyChanged(nameof(IsUserSelected));
            }
        }

        private Visibility _isUserDetailVisible;

        public Visibility IsUserDetailVisible
        {
            get => _isUserDetailVisible;
            set
            {
                _isUserDetailVisible = value;
                OnPropertyChanged(nameof(IsUserDetailVisible));
            }
        }

        private IUserDetailViewModel _selectedDetailViewModel;

        public IUserDetailViewModel SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                this.IsUserSelected = true;

                OnPropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public UserMasterViewModel(IUserModelOperation? model = null)
        {
            this.SwitchToProductMasterPage = new SwitchViewCommand("ProductMasterView");
            this.SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
            this.SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

            this.CreateUser = new OnClickCommand(e => this.StoreUser(), c => this.CanStoreUser());
            this.RemoveUser = new OnClickCommand(e => this.DeleteUser());

            this.Users = new ObservableCollection<IUserDetailViewModel>();

            this._modelOperation = model ?? IUserModelOperation.CreateModelOperation();

            this.IsUserSelected = false;

            Task.Run(this.LoadUsers);
        }

        private bool CanStoreUser()
        {
            return !(
                string.IsNullOrWhiteSpace(this.FirstName) ||
                string.IsNullOrWhiteSpace(this.LastName) ||
                string.IsNullOrWhiteSpace(this.Email) ||
                string.IsNullOrWhiteSpace(this.PhoneNumber) ||
                string.IsNullOrWhiteSpace(this.Balance.ToString()) ||
                this.Balance < 0
            );
        }

        private void StoreUser()
        {
            Task.Run(async () =>
            {
                string guid = Guid.NewGuid().ToString();

                await this._modelOperation.AddAsync(guid, this.FirstName, this.LastName, this.Email, this.Balance, this.PhoneNumber);

                this.LoadUsers();
            });
        }

        private void DeleteUser()
        {
            Task.Run(async () =>
            {
                try
                {
                    await this._modelOperation.DeleteAsync(this.SelectedDetailViewModel.Guid);

                    this.LoadUsers();
                }
                catch (Exception e)
                {

                }
            });
        }

        private async void LoadUsers()
        {
            Dictionary<string, IUserModel> Users = await this._modelOperation.GetAllAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                this._users.Clear();

                foreach (IUserModel u in Users.Values)
                {
                    this._users.Add(new UserDetailViewModel(u.Guid,u.FirstName, u.LastName, u.Email, u.Balance, u.PhoneNumber));
                }
            });

            OnPropertyChanged(nameof(Users));
        }
    }
}