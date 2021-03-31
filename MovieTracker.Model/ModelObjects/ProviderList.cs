using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MovieTracker.Model.ModelObjects
{
    public class ProviderLists : List<ProviderModel>
    {
        public string GroupName { get; set; }
        public ProviderLists(string groupName, IEnumerable<ProviderModel> model) : base(model)
        {
            GroupName = groupName;
        }
    }

    public class ProviderModelDetails
    {
        public string Link { get; set; }

        public IEnumerable<ProviderModel> Flatrate { get; set; }

        public IEnumerable<ProviderModel> Rent { get; set; }

        public IEnumerable<ProviderModel> Buy { get; set; }

        public ProviderModelDetails()
        {
            Link = string.Empty;
            Flatrate = new List<ProviderModel>();
            Rent = new List<ProviderModel>();
            Buy = new List<ProviderModel>();
        }

        public ProviderModelDetails(ProviderDetails providerDetails)
        {
            Link = providerDetails.Link;
            Flatrate = providerDetails.Flatrate?.Select(provider => new ProviderModel(provider));
            Rent = providerDetails.Rent?.Select(provider => new ProviderModel(provider));
            Buy = providerDetails.Buy?.Select(provider => new ProviderModel(provider));
        }
    }

    public class ProviderModel
    {
        public int DisplayPriority { get; set; }

        public Uri LogoPath { get; set; }

        public int ProviderId { get; set; }

        public string ProviderName { get; set; }

        public ProviderModel(Provider provider)
        {
            DisplayPriority = provider.DisplayPriority;
            LogoPath = Utils.ModelUtils.GetImageUri(provider.LogoPath);
            ProviderId = provider.ProviderId;
            ProviderName = provider.ProviderName;
        }
    }

    [DataContract]
    public class ProviderList
    {
        [DataMember(Name = "id")]
        public string MediaId { get; set; }

        [DataMember(Name = "results")]
        public Providers Results { get; set; }
    }

    [DataContract]
    public class Provider
    {
        [DataMember(Name= "display_priority")]
        public int DisplayPriority { get; set; }

        [DataMember(Name = "logo_path")]
        public string LogoPath { get; set; }

        [DataMember(Name = "provider_id")]
        public int ProviderId { get; set; }

        [DataMember(Name = "provider_name")]
        public string ProviderName { get; set; }
    }

    [DataContract]
    public class ProviderDetails
    {
        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "flatrate")]
        public IEnumerable<Provider> Flatrate { get; set; }

        [DataMember(Name = "rent")]
        public IEnumerable<Provider> Rent { get; set; }

        [DataMember(Name = "buy")]
        public IEnumerable<Provider> Buy { get; set; }
    }

    [DataContract]
    public class Providers
    {
        [DataMember(Name = "AR")]
        public ProviderDetails AR { get; set; }

        [DataMember(Name = "AT")]
        public ProviderDetails AT { get; set; }

        [DataMember(Name = "AU")]
        public ProviderDetails AU { get; set; }

        [DataMember(Name = "BE")]
        public ProviderDetails BE { get; set; }

        [DataMember(Name = "BR")]
        public ProviderDetails BR { get; set; }

        [DataMember(Name = "CA")]
        public ProviderDetails CA { get; set; }

        [DataMember(Name = "CH")]
        public ProviderDetails CH { get; set; }

        [DataMember(Name = "CL")]
        public ProviderDetails CL { get; set; }

        [DataMember(Name = "CO")]
        public ProviderDetails CO { get; set; }

        [DataMember(Name = "CZ")]
        public ProviderDetails CZ { get; set; }

        [DataMember(Name = "DE")]
        public ProviderDetails DE { get; set; }

        [DataMember(Name = "DK")]
        public ProviderDetails DK { get; set; }

        [DataMember(Name = "EC")]
        public ProviderDetails EC { get; set; }

        [DataMember(Name = "EE")]
        public ProviderDetails EE { get; set; }

        [DataMember(Name = "ES")]
        public ProviderDetails ES { get; set; }

        [DataMember(Name = "FI")]
        public ProviderDetails FI { get; set; }

        [DataMember(Name = "FR")]
        public ProviderDetails FR { get; set; }

        [DataMember(Name = "GB")]
        public ProviderDetails GB { get; set; }

        [DataMember(Name = "GR")]
        public ProviderDetails GR { get; set; }

        [DataMember(Name = "HU")]
        public ProviderDetails HU { get; set; }

        [DataMember(Name = "ID")]
        public ProviderDetails ID { get; set; }

        [DataMember(Name = "IE")]
        public ProviderDetails IE { get; set; }

        [DataMember(Name = "IN")]
        public ProviderDetails IN { get; set; }

        [DataMember(Name = "IT")]
        public ProviderDetails IT { get; set; }

        [DataMember(Name = "JP")]
        public ProviderDetails JP { get; set; }

        [DataMember(Name = "KR")]
        public ProviderDetails KR { get; set; }

        [DataMember(Name = "LT")]
        public ProviderDetails LT { get; set; }

        [DataMember(Name = "LV")]
        public ProviderDetails LV { get; set; }

        [DataMember(Name = "MX")]
        public ProviderDetails MX { get; set; }

        [DataMember(Name = "MY")]
        public ProviderDetails MY { get; set; }

        [DataMember(Name = "NL")]
        public ProviderDetails NL { get; set; }

        [DataMember(Name = "NO")]
        public ProviderDetails NO { get; set; }

        [DataMember(Name = "NZ")]
        public ProviderDetails NZ { get; set; }

        [DataMember(Name = "PE")]
        public ProviderDetails PE { get; set; }

        [DataMember(Name = "PH")]
        public ProviderDetails PH { get; set; }

        [DataMember(Name = "PL")]
        public ProviderDetails PL { get; set; }

        [DataMember(Name = "PT")]
        public ProviderDetails PT { get; set; }

        [DataMember(Name = "RO")]
        public ProviderDetails RO { get; set; }

        [DataMember(Name = "RU")]
        public ProviderDetails RU { get; set; }

        [DataMember(Name = "SE")]
        public ProviderDetails SE { get; set; }

        [DataMember(Name = "SG")]
        public ProviderDetails SG { get; set; }

        [DataMember(Name = "TH")]
        public ProviderDetails TH { get; set; }

        [DataMember(Name = "TR")]
        public ProviderDetails TR { get; set; }

        [DataMember(Name = "US")]
        public ProviderDetails US { get; set; }

        [DataMember(Name = "VE")]
        public ProviderDetails VE { get; set; }

        [DataMember(Name = "ZA")]
        public ProviderDetails ZA { get; set; }
    }
}
