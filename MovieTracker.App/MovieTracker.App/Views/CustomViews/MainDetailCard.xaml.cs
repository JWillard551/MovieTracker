using MovieTracker.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainDetailCard : ContentView
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty TagLineProperty = BindableProperty.Create(nameof(TagLine), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty RatingProperty = BindableProperty.Create(nameof(Rating), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty TemplatedRatingProperty = BindableProperty.Create(nameof(TemplatedRating), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty RuntimeOrNumSeasonsProperty = BindableProperty.Create(nameof(RuntimeOrNumSeasons), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty ReleaseDateProperty = BindableProperty.Create(nameof(ReleaseDate), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty StatusProperty = BindableProperty.Create(nameof(Status), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty OriginalLanguageProperty = BindableProperty.Create(nameof(OriginalLanguage), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty GenresProperty = BindableProperty.Create(nameof(Genres), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty RadialGaugeBindingContextProperty = BindableProperty.Create(nameof(RadialGaugeBindingContext), typeof(RadialGaugeViewModel), typeof(MainDetailCard), null, BindingMode.OneTime);
        public static readonly BindableProperty UriImageProperty = BindableProperty.Create(nameof(UriImage), typeof(UriImageSource), typeof(MainDetailCard), null, BindingMode.OneTime);
        public static readonly BindableProperty RuntimeLabelVisibleProperty = BindableProperty.Create(nameof(RuntimeLabelVisible), typeof(bool), typeof(MainDetailCard), false, BindingMode.OneTime);
        public static readonly BindableProperty ShowLengthLabelVisibleProperty = BindableProperty.Create(nameof(ShowLengthLabelVisible), typeof(bool), typeof(MainDetailCard), false, BindingMode.OneTime);
        public static readonly BindableProperty YourScoreLayoutVisibleProperty = BindableProperty.Create(nameof(YourScoreLayoutVisible), typeof(bool), typeof(MainDetailCard), false, BindingMode.TwoWay);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string TagLine
        {
            get => (string)GetValue(TagLineProperty);
            set => SetValue(TagLineProperty, value);
        }

        public string Rating
        {
            get => (string)GetValue(RatingProperty);
            set => SetValue(RatingProperty, value);
        }

        public string TemplatedRating
        {
            get => (string)GetValue(TemplatedRatingProperty);
            set => SetValue(TemplatedRatingProperty, value);
        }

        public string RuntimeOrNumSeasons
        {
            get => (string)GetValue(RuntimeOrNumSeasonsProperty);
            set => SetValue(RuntimeOrNumSeasonsProperty, value);
        }

        public string ReleaseDate
        {
            get => (string)GetValue(ReleaseDateProperty);
            set => SetValue(ReleaseDateProperty, value);
        }

        public string Status
        {
            get => (string)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        public string OriginalLanguage
        {
            get => (string)GetValue(OriginalLanguageProperty);
            set => SetValue(OriginalLanguageProperty, value);
        }

        public string Genres
        {
            get => (string)GetValue(GenresProperty);
            set => SetValue(GenresProperty, value);
        }

        public RadialGaugeViewModel RadialGaugeBindingContext
        {
            get => (RadialGaugeViewModel)GetValue(RadialGaugeBindingContextProperty);
            set => SetValue(RadialGaugeBindingContextProperty, value);
        }

        public UriImageSource UriImage
        {
            get => (UriImageSource)GetValue(UriImageProperty);
            set => SetValue(UriImageProperty, value);
        }

        public bool RuntimeLabelVisible
        {
            get => (bool)GetValue(RuntimeLabelVisibleProperty);
            set => SetValue(RuntimeLabelVisibleProperty, value);
        }

        public bool ShowLengthLabelVisible
        {
            get => (bool)GetValue(ShowLengthLabelVisibleProperty);
            set => SetValue(ShowLengthLabelVisibleProperty, value);
        }

        public bool YourScoreLayoutVisible
        {
            get => (bool)GetValue(YourScoreLayoutVisibleProperty);
            set => SetValue(YourScoreLayoutVisibleProperty, value);
        }

        public MainDetailCard()
        {
            InitializeComponent();
        }
    }
}