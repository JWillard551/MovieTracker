using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundTextButton : ContentView
    {
        public event EventHandler Clicked;

        public static readonly BindableProperty RatingProperty = BindableProperty.Create(nameof(Rating), typeof(string), typeof(RoundImageButton), "0");
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(RoundImageButton), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(RoundImageButton), null);

        public string Rating
        {
            get { return (string)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public RoundTextButton()
        {
            InitializeComponent();

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