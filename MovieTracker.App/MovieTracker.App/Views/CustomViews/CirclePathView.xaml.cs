using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CirclePathView : ContentView
    {
        public static BindableProperty MinValueProperty = BindableProperty.Create(nameof(MinValue), typeof(double), typeof(CirclePathView), 1.0d);
        public static BindableProperty MaxValueProperty = BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(CirclePathView), 1.0d);
        public static BindableProperty CurrentProgressProperty = BindableProperty.Create(nameof(CurrentProgress), typeof(double), typeof(CirclePathView), 1.0d);
        public static BindableProperty LineColorProperty = BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(CirclePathView), Color.FromHex("FFFFFF"));

        /// <summary>
        /// The minimum value of the gauge's total progress.
        /// </summary>
        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        /// <summary>
        /// The maximum value of the gauge's total progress.
        /// </summary>
        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        /// <summary>
        /// The gauge's current value to be displayed.
        /// </summary>
        public double CurrentProgress
        {
            get => (double)GetValue(CurrentProgressProperty);
            set => SetValue(CurrentProgressProperty, value);
        }

        /// <summary>
        /// The color of the gauge's progress bar.
        /// </summary>
        public Color LineColor
        {
            get => (Color)GetValue(LineColorProperty);
            set => SetValue(LineColorProperty, value);
        }

        /// <summary>
        /// An action to provide a hook for the renderer to force a redraw on the component.
        /// </summary>
        public Action CurrentProgressChanged { get; set; }

        public CirclePathView()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName != null && propertyName.Equals(CurrentProgressProperty.PropertyName))
                CurrentProgressChanged?.Invoke();
        }
    }
}