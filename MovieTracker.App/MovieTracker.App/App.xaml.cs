﻿using MovieTracker.TMDbModel.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MovieTracker.App
{
    public partial class App : Application
    {
        const int SMALLWIDTHRESOLUTION = 768;
        const int SMALLHEIGHTRESOLUTION = 1280;

        public App()
        {
            InitializeComponent();
            LoadDeviceStyles();

            DependencyService.Register<TMDbService>();
            MainPage = new AppShell();
        }

        public static bool IsSmallDevice()
        {
            return (DeviceDisplay.MainDisplayInfo.Width <= SMALLWIDTHRESOLUTION 
                && DeviceDisplay.MainDisplayInfo.Height <= SMALLHEIGHTRESOLUTION);
        }

        private void LoadDeviceStyles()
        {
            if (IsSmallDevice())
                primaryDictionary.MergedDictionaries.Add(SmallDeviceStyle.SharedInstance);
            else
                primaryDictionary.MergedDictionaries.Add(GeneralDevicesStyle.SharedInstance);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
