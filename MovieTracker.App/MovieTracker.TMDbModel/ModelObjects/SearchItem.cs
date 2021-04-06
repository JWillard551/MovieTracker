using MovieTracker.TMDbModel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieTracker.TMDbModel.ModelObjects
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

        public SearchItem()
        {
            //MediaType = MediaType.Unspecified;
            ResultName = "UNKNOWN";
            ReleaseDate = null;
            Overview = "UNKNOWN";
            Rating = 0;
        }

        public SearchItem(SearchBase baseItem)
        {
            Id = baseItem.Id;
            MediaType = baseItem.MediaType;
        }

        public SearchItem(SearchMovie movie)
        {
            Id = movie.Id;
            ResultName = movie.OriginalTitle;
            ReleaseDate = movie.ReleaseDate.HasValue ? movie.ReleaseDate.Value : DateTime.Now;
            Overview = movie.Overview;
            Rating = movie.VoteAverage;
            ImageUri = ModelUtils.GetImageUri(movie.PosterPath ?? string.Empty);
        }

        public SearchItem(SearchTv show)
        {
            Id = show.Id;
            ResultName = show.OriginalName;
            ReleaseDate = show.FirstAirDate;
            Overview = show.Overview;
            Rating = show.VoteAverage;
            ImageUri = ModelUtils.GetImageUri(show.PosterPath ?? string.Empty);
        }

        public SearchItem(SearchPerson person)
        {
            Id = person.Id;
            ResultName = person.Name;
            Overview = "Overview";
            ImageUri = ModelUtils.GetImageUri(person.ProfilePath ?? string.Empty);
        }
    }
}
