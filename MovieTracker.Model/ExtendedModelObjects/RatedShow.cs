using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.TMDb;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Model.ExtendedModelObjects
{
    [DataContract]
    public class RatedShow
    {
        [DataMember(Name="backdrop_path")]
        public string BackdropPath { get; set; }

        [DataMember(Name="first_air_date")]
        public DateTimeOffset FirstAirDate { get; set; }

        [DataMember(Name="genre_ids")]
        public long[] GenreIds { get; set; }

        [DataMember(Name="id")]
        public long Id { get; set; }

        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name="origin_country")]
        public string[] OriginCountry { get; set; }

        [DataMember(Name="original_language")]
        public string OriginalLanguage { get; set; }

        [DataMember(Name="original_name")]
        public string OriginalName { get; set; }

        [DataMember(Name="overview")]
        public string Overview { get; set; }

        [DataMember(Name="poster_path")]
        public string PosterPath { get; set; }

        [DataMember(Name="vote_average")]
        public double VoteAverage { get; set; }

        [DataMember(Name="vote_count")]
        public long VoteCount { get; set; }

        [DataMember(Name="popularity")]
        public double Popularity { get; set; }

        [DataMember(Name="rating")]
        public long Rating { get; set; }
    }

    [DataContract]
    public class RatedShows : PagedResult<RatedShow>
    {

    }
}
