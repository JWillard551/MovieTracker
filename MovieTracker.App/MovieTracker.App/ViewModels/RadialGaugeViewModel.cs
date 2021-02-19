using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class RadialGaugeViewModel : BaseViewModel //ExtendedBindableObject
    {
        private string _detail;
        public string Detail 
        { 
            get => _detail; 
            set => SetProperty(ref _detail, value); 
        }

        private double _minValue;
        public double MinValue
        {
            get => _minValue;
            set => SetProperty(ref _minValue, value);
        }

        private double _maxValue;
        public double MaxValue
        {
            get => _maxValue;
            set => SetProperty(ref _maxValue, value);
        }

        private double _currentProgress;
        public double CurrentProgress
        {
            get => _currentProgress;
            set => SetProperty(ref _currentProgress, value);
        }

        public RadialGaugeViewModel() { }
    }

    //public class ExtendedBindableObject : BindableObject
    //{
    //    public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    //    {
    //        if (EqualityComparer<T>.Default.Equals(storage, value))
    //            return false;

    //        storage = value;
    //        OnPropertyChanged(propertyName);
    //        return true;
    //    }
    //}
}
