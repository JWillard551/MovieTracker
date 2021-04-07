using System.Collections.Generic;

namespace MovieTracker.TMDbModel.AdditionalModelObjects
{
    public class ProviderLists : List<ProviderItem>
    {
        public string GroupName { get; set; }

        public ProviderLists(string groupName, List<ProviderItem> list) : base(list)
        {
            GroupName = groupName;
        }
    }
}
