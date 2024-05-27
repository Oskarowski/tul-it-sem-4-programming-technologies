using Presentation.Model.API;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System;

namespace Presentation.ViewModel
{
    internal class ProductMasterViewModel : IViewModel, IProductMasterViewModel
    {
        public ICommand SwitchToUserMasterPage { get; set; }

        public ICommand SwitchToStateMasterPage { get; set; }

        public ICommand SwitchToEventMasterPage { get; set; }

        public ICommand CreateProduct { get; set; }

        public ICommand RemoveProduct { get; set; }

        private readonly IProductModelOperation _modelOperation;

        private readonly IErrorInformer _informer;

        private ObservableCollection<IProductDetailViewModel> _products;

        public ObservableCollection<IProductDetailViewModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private string _name;
        private double _price;
        private string _author;
        private string _publisher;
        private int _pages;
        private DateTime _publicationDate;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        public string Publisher
        {
            get => _publisher;
            set
            {
                _publisher = value;
                OnPropertyChanged(nameof(Publisher));
            }
        }

        public int Pages
        {
            get => _pages;
            set
            {
                _pages = value;
                OnPropertyChanged(nameof(Pages));
            }
        }

        public DateTime PublicationDate
        {
            get => _publicationDate;
            set
            {
                _publicationDate = value;
                OnPropertyChanged(nameof(PublicationDate));
            }
        }

        private bool _isProductSelected;

        public bool IsProductSelected
        {
            get => _isProductSelected;
            set
            {
                this.IsProductDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

                _isProductSelected = value;
                OnPropertyChanged(nameof(IsProductSelected));
            }
        }

        private Visibility _isProductDetailVisible;

        public Visibility IsProductDetailVisible
        {
            get => _isProductDetailVisible;
            set
            {
                _isProductDetailVisible = value;
                OnPropertyChanged(nameof(IsProductDetailVisible));
            }
        }

        private IProductDetailViewModel _selectedDetailViewModel;

        public IProductDetailViewModel SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                this.IsProductSelected = true;

                OnPropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public ProductMasterViewModel(IProductModelOperation? model = null, IErrorInformer? informer = null)
        {
            this.SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
            this.SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
            this.SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

            this.CreateProduct = new OnClickCommand(e => this.StoreProduct(), c => this.CanStoreProduct());
            this.RemoveProduct = new OnClickCommand(e => this.DeleteProduct());

            this.Products = new ObservableCollection<IProductDetailViewModel>();

            this._modelOperation = model ?? IProductModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();

            this.IsProductSelected = false;

            Task.Run(this.LoadProducts);
        }

        private bool CanStoreProduct()
        {
            return !(
                string.IsNullOrWhiteSpace(this.Name) ||
                string.IsNullOrWhiteSpace(this.Price.ToString()) ||
                string.IsNullOrWhiteSpace(this.Author) ||
                string.IsNullOrWhiteSpace(this.Publisher) ||
                string.IsNullOrWhiteSpace(this.Pages.ToString()) ||
                string.IsNullOrWhiteSpace(this.PublicationDate.ToString()) ||
                this.Price <= 0 ||
                this.Pages <= 0
            );
        }

        private void StoreProduct()
        {
            Task.Run(async () =>
            {
                string guid = Guid.NewGuid().ToString();

                await this._modelOperation.AddAsync(guid, this.Name, this.Price, this.Author, this.Publisher, this.Pages, this.PublicationDate);

                this.LoadProducts();

                this._informer.InformSuccess("Product added successfully!");

            });
        }

        private void DeleteProduct()
        {
            Task.Run(async () =>
            {
                try
                {
                    await this._modelOperation.DeleteAsync(this.SelectedDetailViewModel.Guid);

                    this.LoadProducts();

                    this._informer.InformSuccess("Product deleted successfully!");
                }
                catch (Exception e)
                {
                    this._informer.InformError("Error while deleting product!");
                }
            });
        }

        private async void LoadProducts()
        {
            Dictionary<string, IProductModel> Products = await this._modelOperation.GetAllAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                this._products.Clear();

                foreach (IProductModel p in Products.Values)
                {
                    this._products.Add(new ProductDetailViewModel(p.Guid, p.Name, p.Price, p.Author, p.Publisher, p.Pages, p.PublicationDate));
                }
            });

            OnPropertyChanged(nameof(Products));
        }
    }
}
