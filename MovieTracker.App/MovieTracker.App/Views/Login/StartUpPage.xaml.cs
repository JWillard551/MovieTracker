using MovieTracker.App.ViewModels.LoginViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartUpPage : ContentPage
    {
        internal StartUpViewModel _viewModel;
        public StartUpPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new StartUpViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Init();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}