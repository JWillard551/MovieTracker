﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SharedResources : ResourceDictionary
    {
        public SharedResources()
        {
            InitializeComponent();
        }
    }
}