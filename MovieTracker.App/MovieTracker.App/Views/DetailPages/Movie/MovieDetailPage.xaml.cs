using FFImageLoading.Forms;
using MovieTracker.App.ViewModels.DetailViewModels.Movie;
using MovieTracker.TMDbModel.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.DetailPages.Movie
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailPage : ContentPage
    {
        public MovieDetailPage()
        {
            InitializeComponent();
        }
    }
}