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

        //Button Color / State
        //public static readonly BindableProperty FavoritesButtonColorStateProperty = BindableProperty.Create(nameof(FavoritesButtonColorState), typeof(Color), typeof(DetailButtons), Color.FromHex("032541"));
        //public static readonly BindableProperty RatingButtonColorStateProperty = BindableProperty.Create(nameof(RatingButtonColorState), typeof(Color), typeof(DetailButtons), Color.FromHex("032541"));
        //public static readonly BindableProperty WatchlistButtonColorStateProperty = BindableProperty.Create(nameof(WatchlistButtonColorState), typeof(Color), typeof(DetailButtons), Color.FromHex("032541"));

        public static readonly BindableProperty FavoritesButtonStateProperty = BindableProperty.Create(nameof(FavoritesButtonState), typeof(bool), typeof(DetailButtons), false);
        public static readonly BindableProperty RatingsButtonStateProperty = BindableProperty.Create(nameof(RatingsButtonState), typeof(bool), typeof(DetailButtons), false);
        public static readonly BindableProperty WatchlistButtonStateProperty = BindableProperty.Create(nameof(WatchlistButtonState), typeof(bool), typeof(DetailButtons), false);


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

        //public Color FavoritesButtonColorState
        //{
        //    get { return (Color)GetValue(FavoritesButtonColorStateProperty); }
        //    set { SetValue(FavoritesButtonColorStateProperty, value); }
        //}

        //public Color WatchlistButtonColorState
        //{
        //    get { return (Color)GetValue(WatchlistButtonColorStateProperty); }
        //    set { SetValue(WatchlistButtonColorStateProperty, value); }
        //}

        //public Color RatingButtonColorState
        //{
        //    get { return (Color)GetValue(RatingButtonColorStateProperty); }
        //    set { SetValue(RatingButtonColorStateProperty, value); }
        //}

        public bool FavoritesButtonState
        {
            get { return (bool)GetValue(FavoritesButtonStateProperty); }
            set { SetValue(FavoritesButtonStateProperty, value); }
        }

        public bool WatchlistButtonState
        {
            get { return (bool)GetValue(WatchlistButtonStateProperty); }
            set { SetValue(WatchlistButtonStateProperty, value); }
        }

        public bool RatingsButtonState
        {
            get { return (bool)GetValue(RatingsButtonStateProperty); }
            set { SetValue(RatingsButtonStateProperty, value); }
        }

        public DetailButtons()
        {
            InitializeComponent();
        }
    }
}