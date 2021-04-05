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
    public class RatedMovie
    {
        [DataMember(Name="genre_ids")]
        public long[] GenreIds { get; set; }

        [DataMember(Name="title")]
        public string Title { get; set; }

        [DataMember(Name="original_language")]
        public string OriginalLanguage { get; set; }

        [DataMember(Name="original_title")]
        public string OriginalTitle { get; set; }

        [DataMember(Name="poster_path")]
        public string PosterPath { get; set; }

        [DataMember(Name="video")]
        public bool Video { get; set; }

        [DataMember(Name="vote_average")]
        public double VoteAverage { get; set; }

        [DataMember(Name="overview")]
        public string Overview { get; set; }

        [DataMember(Name="release_date")]
        public DateTimeOffset ReleaseDate { get; set; }

        [DataMember(Name="vote_count")]
        public long VoteCount { get; set; }

        [DataMember(Name="id")]
        public long Id { get; set; }

        [DataMember(Name="adult")]
        public bool Adult { get; set; }

        [DataMember(Name="backdrop_path")]
        public string BackdropPath { get; set; }

        [DataMember(Name="popularity")]
        public double Popularity { get; set; }

        [DataMember(Name="rating")]
        public long Rating { get; set; }
    }

    [DataContract]
    public class RatedMovies : PagedResult<RatedMovie>
    {

    }
}
