using MovieTracker.App.ViewModels.TabbedPageViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ShowID), nameof(ShowID))]
    public partial class ShowTabbedPage : TabbedPage
    {
        private string _showID;
        private ShowTabbedPageViewModel _viewModel;
        public string ShowID
        {
            get => _showID;
            set
            {
                _showID = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
            }
        }
        public ShowTabbedPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel = new ShowTabbedPageViewModel(Convert.ToInt32(ShowID));
            await _viewModel.Initialization;
            BindingContext = _viewModel;
            showDetail.BindingContext = _viewModel.Tab1;
            castCrew.BindingContext = _viewModel.Tab2;
            watchOn.BindingContext = _viewModel.Tab3;
            _viewModel.Tab1.IsLoaded = true;
        }
    }
}