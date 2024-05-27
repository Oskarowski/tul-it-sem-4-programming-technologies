namespace Presentation.ViewModel
{
    internal class MainWindowViewModel : IViewModel
    {
        private IViewModel _selectedViewModel { get; set; }


        public MainWindowViewModel()
        {
            this._selectedViewModel = new HomeViewModel();
        }

        public new IViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;

                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
    }
}
