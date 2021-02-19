using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OvalFrame : Frame
    {
        public OvalFrame()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(HeightProperty.PropertyName))
                CornerRadius = (float)Height / 2; //Creates a perfect circle using the corner radius of a frame object.
        }
    }
}