using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Threading;
using MovieTracker.Model.ModelEnums;

namespace MovieTracker.Model.Client
{
    public interface IProviderInfo
    {
        Task<ProviderList> GetProvidersAsync(int id, MediaType mediaType, CancellationToken cancellationToken = default(CancellationToken));
    }

    [DataContract]
    public class ProviderDetail
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
    public class ProviderInfo
    {
        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "flatrate")]
        public IEnumerable<ProviderDetail> Flatrate { get; set; }

        [DataMember(Name = "rent")]
        public IEnumerable<ProviderDetail> Rent { get; set; }

        [DataMember(Name = "buy")]
        public IEnumerable<ProviderDetail> Buy { get; set; }
    }

    [DataContract]
    public class ProviderList
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "results")]
        public Providers Results { get; set; }
    }

    [DataContract]
    public class Providers
    {
        [DataMember(Name = "AR")]
        public ProviderInfo AR { get; set; }

        [DataMember(Name = "AT")]
        public ProviderInfo AT { get; set; }

        [DataMember(Name = "AU")]
        public ProviderInfo AU { get; set; }

        [DataMember(Name = "BE")]
        public ProviderInfo BE { get; set; }

        [DataMember(Name = "BR")]
        public ProviderInfo BR { get; set; }

        [DataMember(Name = "CA")]
        public ProviderInfo CA { get; set; }

        [DataMember(Name = "CH")]
        public ProviderInfo CH { get; set; }

        [DataMember(Name = "CL")]
        public ProviderInfo CL { get; set; }

        [DataMember(Name = "CO")]
        public ProviderInfo CO { get; set; }

        [DataMember(Name = "CZ")]
        public ProviderInfo CZ { get; set; }

        [DataMember(Name = "DE")]
        public ProviderInfo DE { get; set; }

        [DataMember(Name = "DK")]
        public ProviderInfo DK { get; set; }

        [DataMember(Name = "EC")]
        public ProviderInfo EC { get; set; }

        [DataMember(Name = "EE")]
        public ProviderInfo EE { get; set; }

        [DataMember(Name = "ES")]
        public ProviderInfo ES { get; set; }

        [DataMember(Name = "FI")]
        public ProviderInfo FI { get; set; }

        [DataMember(Name = "FR")]
        public ProviderInfo FR { get; set; }

        [DataMember(Name = "GB")]
        public ProviderInfo GB { get; set; }

        [DataMember(Name = "GR")]
        public ProviderInfo GR { get; set; }

        [DataMember(Name = "HU")]
        public ProviderInfo HU { get; set; }

        [DataMember(Name = "ID")]
        public ProviderInfo ID { get; set; }

        [DataMember(Name = "IE")]
        public ProviderInfo IE { get; set; }

        [DataMember(Name = "IN")]
        public ProviderInfo IN { get; set; }

        [DataMember(Name = "IT")]
        public ProviderInfo IT { get; set; }

        [DataMember(Name = "JP")]
        public ProviderInfo JP { get; set; }

        [DataMember(Name = "KR")]
        public ProviderInfo KR { get; set; }

        [DataMember(Name = "LT")]
        public ProviderInfo LT { get; set; }

        [DataMember(Name = "LV")]
        public ProviderInfo LV { get; set; }

        [DataMember(Name = "MX")]
        public ProviderInfo MX { get; set; }

        [DataMember(Name = "MY")]
        public ProviderInfo MY { get; set; }

        [DataMember(Name = "NL")]
        public ProviderInfo NL { get; set; }

        [DataMember(Name = "NO")]
        public ProviderInfo NO { get; set; }

        [DataMember(Name = "NZ")]
        public ProviderInfo NZ { get; set; }

        [DataMember(Name = "PE")]
        public ProviderInfo PE { get; set; }

        [DataMember(Name = "PH")]
        public ProviderInfo PH { get; set; }

        [DataMember(Name = "PL")]
        public ProviderInfo PL { get; set; }

        [DataMember(Name = "PT")]
        public ProviderInfo PT { get; set; }

        [DataMember(Name = "RO")]
        public ProviderInfo RO { get; set; }

        [DataMember(Name = "RU")]
        public ProviderInfo RU { get; set; }

        [DataMember(Name = "SE")]
        public ProviderInfo SE { get; set; }

        [DataMember(Name = "SG")]
        public ProviderInfo SG { get; set; }

        [DataMember(Name = "TH")]
        public ProviderInfo TH { get; set; }

        [DataMember(Name = "TR")]
        public ProviderInfo TR { get; set; }

        [DataMember(Name = "US")]
        public ProviderInfo US { get; set; }

        [DataMember(Name = "VE")]
        public ProviderInfo VE { get; set; }

        [DataMember(Name = "ZA")]
        public ProviderInfo ZA { get; set; }
    }
}
