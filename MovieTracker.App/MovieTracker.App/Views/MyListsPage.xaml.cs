using MovieTracker.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyListsPage : ContentPage
    {
        public MyListsPageViewModel _viewModel;
        public MyListsPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel = new MyListsPageViewModel();
            await _viewModel.Initialization;
            BindingContext = _viewModel;
        }
    }
}