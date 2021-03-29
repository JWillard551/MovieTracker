﻿using MovieTracker.App.Services;
using MovieTracker.App.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            DependencyService.Register<MockDataStore>();
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
