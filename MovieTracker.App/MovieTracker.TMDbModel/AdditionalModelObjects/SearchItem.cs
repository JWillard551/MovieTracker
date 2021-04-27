using MovieTracker.TMDbModel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieTracker.TMDbModel.AdditionalModelObjects
{
    public class SearchItem
    {
        public int Id { get; set; }
        public string ResultName { get; set; }
        public string Overview { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public double Rating { get; set; }
        public Uri ImageUri { get; set; }
        public MediaType MediaType { get; set; }

        public SearchItem(SearchBase baseItem)
        {
            MediaType = MediaType.Unknown;
            Id = baseItem.Id;
            MediaType = baseItem.MediaType;
            ResultName = "UNKNOWN";
            ReleaseDate = null;
            Overview = "UNKNOWN";
            Rating = 0;
        }

        public SearchItem(SearchMovie movie)
        {
            MediaType = MediaType.Movie;
            Id = movie.Id;
            ResultName = movie.OriginalTitle;
            ReleaseDate = movie.ReleaseDate.HasValue ? movie.ReleaseDate.Value : DateTime.Now;
            Overview = movie.Overview;
            Rating = movie.VoteAverage;
            ImageUri = ModelUtils.GetImageUri(movie?.PosterPath);
        }

        public SearchItem(SearchTv show)
        {
            MediaType = MediaType.Tv;
            Id = show.Id;
            ResultName = show.OriginalName;
            ReleaseDate = show.FirstAirDate;
            Overview = show.Overview;
            Rating = show.VoteAverage;
            ImageUri = ModelUtils.GetImageUri(show?.PosterPath);
        }

        public SearchItem(SearchPerson person)
        {
            MediaType = MediaType.Person;
            Id = person.Id;
            ResultName = person.Name;
            ImageUri = ModelUtils.GetImageUri(person?.ProfilePath);
        }
    }
}
