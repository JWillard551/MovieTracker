using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieShowTitleView : ContentView
    {
        public static readonly BindableProperty MediaRatingProperty = BindableProperty.Create(nameof(MediaRating), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty MediaTitleProperty = BindableProperty.Create(nameof(MediaTitle), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);
        public static readonly BindableProperty MediaDateProperty = BindableProperty.Create(nameof(MediaDate), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);

        public MovieShowTitleView()
        {
            InitializeComponent();
        }

        public string MediaRating
        {
            get => (string)GetValue(MediaRatingProperty);
            set => SetValue(MediaRatingProperty, value);
        }

        public string MediaTitle
        {
            get => (string)GetValue(MediaTitleProperty);
            set => SetValue(MediaTitleProperty, value);
        }

        public string MediaDate
        {
            get => (string)GetValue(MediaDateProperty);
            set => SetValue(MediaDateProperty, value);
        }
    }
}