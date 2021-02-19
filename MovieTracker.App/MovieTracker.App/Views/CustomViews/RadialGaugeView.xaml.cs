using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadialGaugeView : ContentView
    {
        public RadialGaugeView()
        {
            InitializeComponent();
        }
    }
}