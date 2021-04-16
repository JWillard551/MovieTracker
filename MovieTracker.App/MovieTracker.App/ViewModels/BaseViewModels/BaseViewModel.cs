using MovieTracker.App.Services;
using MovieTracker.TMDbModel.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.BaseViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region DI Services

        protected ITMDbService TMDbService => DependencyService.Get<ITMDbService>();
        protected IMessage ToastService => DependencyService.Get<IMessage>();
        protected IUserPromptService UserPromptService => DependencyService.Get<IUserPromptService>();

        #endregion

        #region Base Properties

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        bool isLoaded = false;
        public bool IsLoaded
        {
            get { return isLoaded; }
            set { SetProperty(ref isLoaded, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        #endregion

        public BaseViewModel() { }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
