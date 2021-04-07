using MovieTracker.TMDbModel.Utils;
using System;
using TMDbLib.Objects.General;

namespace MovieTracker.TMDbModel.AdditionalModelObjects
{
    public class ProviderItem
    {
        public int? DisplayPriority { get; set; }
        public Uri LogoPath { get; set; }
        public int? ProviderId { get; set; }
        public string ProviderName { get; set; }

        public ProviderItem(WatchProviderItem providerItem)
        {
            DisplayPriority = providerItem.DisplayPriority;
            LogoPath = ModelUtils.GetImageUri(providerItem.LogoPath);
            ProviderId = providerItem.ProviderId;
            ProviderName = providerItem.ProviderName;
        }
    }
}
