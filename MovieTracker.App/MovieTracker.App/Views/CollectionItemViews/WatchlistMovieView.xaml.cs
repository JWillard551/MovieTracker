using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.CollectionItemViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchlistMovieView : ContentView
    {
        public WatchlistMovieView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            //Some notes on why we're doing this here:
            //1. It's faster than XAML bindings.
            //2. Because it's faster, it handles scenarios with FFImageLoading sometimes (or always) repeating images on a collection view.
            //   as the null set on source helps prevent this from occurring.
            cachedImage.Source = null;
            var item = BindingContext as MovieItemViewModel;
            if (item == null)
                return;

            cachedImage.Source = item.Movie.Poster;
            base.OnBindingContextChanged();
        }
    }
}