using MovieTracker.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static readonly BindableProperty RuntimeOrNumSeasonsProperty = BindableProperty.Create(nameof(RuntimeOrNumSeasons), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty ReleaseDateProperty = BindableProperty.Create(nameof(ReleaseDate), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty StatusProperty = BindableProperty.Create(nameof(Status), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty OriginalLanguageProperty = BindableProperty.Create(nameof(OriginalLanguage), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty GenresProperty = BindableProperty.Create(nameof(Genres), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty RadialGaugeBindingContextProperty = BindableProperty.Create(nameof(RadialGaugeBindingContext), typeof(RadialGaugeViewModel), typeof(MainDetailCard), null, BindingMode.OneTime);
        public static readonly BindableProperty UriImageProperty = BindableProperty.Create(nameof(UriImage), typeof(UriImageSource), typeof(MainDetailCard), null, BindingMode.OneTime);

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

        public MainDetailCard()
        {
            InitializeComponent();
        }
    }
}