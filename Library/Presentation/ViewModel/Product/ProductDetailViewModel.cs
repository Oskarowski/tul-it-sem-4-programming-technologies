using Presentation.Model.API;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    internal class ProductDetailViewModel : IViewModel, IProductDetailViewModel
    {
        public ICommand UpdateProduct { get; set; }

        private readonly IProductModelOperation _modelOperation;

        private readonly IErrorInformer _informer;

        private string _guid;
        private string _name;
        private double _price;
        private string _author;
        private string _publisher;
        private int _pages;
        private DateTime _publicationDate;

        public string Guid
        {
            get => _guid;
            set
            {
                _guid = value;
                OnPropertyChanged(nameof(Guid));
            }
        }

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

        public ProductDetailViewModel(IProductModelOperation? model = null, IErrorInformer? informer = null)
        {
            this.UpdateProduct = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = model ?? IProductModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();
        }

        public ProductDetailViewModel(string guid, string name, double price, string author, string publisher, int pages, DateTime publicationDate, IProductModelOperation? model = null, IErrorInformer? informer = null)
        {
            Guid = guid;
            Name = name;
            Price = price;
            Author = author;
            Publisher = publisher;
            Pages = pages;
            PublicationDate = publicationDate;

            this.UpdateProduct = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = model ?? IProductModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();
        }

        private void Update()
        {
            Task.Run(() =>
            {
                this._modelOperation.UpdateAsync(this.Guid, this.Name, this.Price, this.Author, this.Publisher, this.Pages, this.PublicationDate);

                this._informer.InformSuccess("Product successfully updated!");
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(this.Name) ||
                string.IsNullOrWhiteSpace(this.Author) ||
                string.IsNullOrWhiteSpace(this.Publisher) ||
                string.IsNullOrWhiteSpace(this.Price.ToString()) ||
                string.IsNullOrWhiteSpace(this.Pages.ToString()) ||
                string.IsNullOrWhiteSpace(this.PublicationDate.ToString()) ||
                this.Price <= 0 || 
                this.Pages <= 0
            );
        }
    }
}
