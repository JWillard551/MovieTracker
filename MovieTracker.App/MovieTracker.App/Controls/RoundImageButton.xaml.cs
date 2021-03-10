using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundImageButton : ContentView
    {
        public event EventHandler Clicked;

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(RoundImageButton), null);
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create("Source", typeof(ImageSource), typeof(RoundImageButton), default(RoundImageButton));
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(RoundImageButton), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public RoundImageButton()
        {
            InitializeComponent();

            buttonImage.SetBinding(Image.SourceProperty, new Binding("Source", source: this));

            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => {
                    Clicked?.Invoke(this, EventArgs.Empty);

                    if (Command != null && Command.CanExecute(CommandParameter))
                       Command.Execute(CommandParameter);
                })
            });
        }
    }
}