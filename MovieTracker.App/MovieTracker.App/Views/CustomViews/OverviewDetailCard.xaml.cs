using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewDetailCard : ContentView
    {
        public static readonly BindableProperty OverviewProperty = BindableProperty.Create(nameof(Overview), typeof(string), typeof(MainDetailCard), string.Empty, BindingMode.OneTime);

        public string Overview
        {
            get => (string)GetValue(OverviewProperty);
            set => SetValue(OverviewProperty, value);
        }

        public OverviewDetailCard()
        {
            InitializeComponent();
        }
    }
}