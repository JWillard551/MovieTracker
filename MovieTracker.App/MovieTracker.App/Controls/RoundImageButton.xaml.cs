using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundImageButton : ContentView
    {
        public event EventHandler Clicked;

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(RoundImageButton), null);
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(RoundImageButton), default(RoundImageButton));
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(RoundImageButton), null);
        public static readonly BindableProperty SelectedColorStateProperty = BindableProperty.Create(nameof(SelectedColorState), typeof(Color), typeof(RoundImageButton), Color.FromHex("032541"));
        public static readonly BindableProperty SelectedStateProperty = BindableProperty.Create(nameof(SelectedState), typeof(bool), typeof(RoundImageButton), false);


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public Color SelectedColorState
        {
            get { return (Color)GetValue(SelectedColorStateProperty); }
            set { SetValue(SelectedColorStateProperty, value); }
        }

        public bool SelectedState
        {
            get { return (bool)GetValue(SelectedStateProperty); }
            set { SetValue(SelectedStateProperty, value); }
        }

        public RoundImageButton()
        {
            InitializeComponent();

            buttonImage.SetBinding(Image.SourceProperty, new Binding(nameof(ImageSource), source: this));

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