using MovieTracker.App.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MovieTracker.App.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}