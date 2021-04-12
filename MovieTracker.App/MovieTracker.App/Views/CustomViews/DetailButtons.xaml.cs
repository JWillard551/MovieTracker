using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailButtons : ContentView
    {
        public static readonly BindableProperty PlayTrailerCommandProperty = BindableProperty.Create(nameof(PlayTrailerCommand), typeof(ICommand), typeof(DetailButtons), null);
        public static readonly BindableProperty AddToWatchlistCommandProperty = BindableProperty.Create(nameof(AddToWatchlistCommand), typeof(ICommand), typeof(DetailButtons), null);
        public static readonly BindableProperty AddToFavoritesCommandProperty = BindableProperty.Create(nameof(AddToFavoritesCommand), typeof(ICommand), typeof(DetailButtons), null);
        public static readonly BindableProperty RateCommandProperty = BindableProperty.Create(nameof(RateCommand), typeof(ICommand), typeof(DetailButtons), null);
        public static readonly BindableProperty PlayTrailerButtonVisibleProperty = BindableProperty.Create(nameof(PlayTrailerButtonVisible), typeof(bool), typeof(DetailButtons), true);
        public static readonly BindableProperty AddToWatchlistButtonVisibleProperty = BindableProperty.Create(nameof(AddToWatchlistButtonVisible), typeof(bool), typeof(DetailButtons), true);
        public static readonly BindableProperty AddToFavoritesButtonVisibleProperty = BindableProperty.Create(nameof(AddToFavoritesButtonVisible), typeof(bool), typeof(DetailButtons), true);
        public static readonly BindableProperty RateCommandButtonVisibleProperty = BindableProperty.Create(nameof(RateButtonVisible), typeof(bool), typeof(DetailButtons), true);

        public ICommand PlayTrailerCommand
        {
            get { return (ICommand)GetValue(PlayTrailerCommandProperty); }
            set { SetValue(PlayTrailerCommandProperty, value); }
        }

        public ICommand AddToWatchlistCommand
        {
            get { return (ICommand)GetValue(AddToWatchlistCommandProperty); }
            set { SetValue(AddToWatchlistCommandProperty, value); }
        }

        public ICommand AddToFavoritesCommand
        {
            get { return (ICommand)GetValue(AddToFavoritesCommandProperty); }
            set { SetValue(AddToFavoritesCommandProperty, value); }
        }

        public ICommand RateCommand
        {
            get { return (ICommand)GetValue(RateCommandProperty); }
            set { SetValue(RateCommandProperty, value); }
        }

        public bool PlayTrailerButtonVisible
        {
            get { return (bool)GetValue(PlayTrailerButtonVisibleProperty); }
            set { SetValue(PlayTrailerButtonVisibleProperty, value); }
        }

        public bool AddToWatchlistButtonVisible
        {
            get { return (bool)GetValue(AddToWatchlistButtonVisibleProperty); }
            set { SetValue(AddToWatchlistButtonVisibleProperty, value); }
        }

        public bool AddToFavoritesButtonVisible
        {
            get { return (bool)GetValue(AddToFavoritesButtonVisibleProperty); }
            set { SetValue(AddToFavoritesButtonVisibleProperty, value); }
        }

        public bool RateButtonVisible
        {
            get { return (bool)GetValue(RateCommandButtonVisibleProperty); }
            set { SetValue(RateCommandButtonVisibleProperty, value); }
        }

        public DetailButtons()
        {
            InitializeComponent();
        }
    }
}