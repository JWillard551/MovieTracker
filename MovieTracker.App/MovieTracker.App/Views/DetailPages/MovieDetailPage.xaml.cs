﻿using MovieTracker.App.ViewModels.DetailViewModels;
using MovieTracker.TMDbModel.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.DetailPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailPage : ContentPage
    {
        public MovieDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            cachedImage.Source = null;
            var item = BindingContext as MovieDetailViewModel;
            if (item == null)
                return;

            cachedImage.Source = new UriImageSource() { Uri = ModelUtils.GetImageUri(item.MovieInfo.PosterPath) };
            base.OnBindingContextChanged();
        }
    }
}