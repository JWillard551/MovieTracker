using MovieTracker.Model.ModelEnums;
using MovieTracker.Model.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.TMDb;

namespace MovieTracker.Model.ModelObjects
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string ResultName { get; set; }
        public string Overview { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public decimal Rating { get; set; }
        public Uri ImageUri { get; set; }
        public List<Genre> Genres { get; set; }

        public MediaType MediaType { get; set; }

        protected bool HasRating
        {
            get { return Rating > 0; }
        }
        
        protected bool HasReleaseDate
        {
            get { return ReleaseDate != null; }
        }

        public SearchResult() 
        {
            MediaType = MediaType.Unspecified;
            ResultName = "UNKNOWN";
            ReleaseDate = null;
            Overview = "UNKNOWN";
            Rating = 0;
        }

        public SearchResult(System.Net.TMDb.Movie movie)
        {
            Id = movie.Id;
            ResultName = movie.OriginalTitle;
            ReleaseDate = movie.ReleaseDate.HasValue ? movie.ReleaseDate.Value : DateTime.Now;
            Overview = movie.Overview;
            Rating = CalculateRating(movie.VoteAverage);
            Genres = movie.Genres?.Select(genre => new Genre(genre)).ToList();
            MediaType = MediaType.Movie;
            ImageUri = ModelUtils.GetImageUri(movie.Poster ?? string.Empty);
        }

        public SearchResult(System.Net.TMDb.Show show)
        {
            Id = show.Id;
            ResultName = show.OriginalName;
            ReleaseDate = DetermineShowReleaseDate(show.FirstAirDate, show.LastAirDate);
            Overview = show.Overview;
            Rating = CalculateRating(show.VoteAverage);
            MediaType = MediaType.Show;
            ImageUri = ModelUtils.GetImageUri(show.Poster ?? string.Empty);
        }

        public SearchResult(System.Net.TMDb.Person person)
        {
            Id = person.Id;
            ResultName = person.Name;
            Overview = person.Biography;
            MediaType = MediaType.Person;
            ImageUri = ModelUtils.GetImageUri(person.Poster ?? string.Empty);
        }

        protected decimal CalculateRating(decimal averageVoteValue)
        {
            return decimal.Round(averageVoteValue * 10, 0);
        }

        protected DateTime? DetermineShowReleaseDate(DateTime? firstAirDate, DateTime? lastAirDate)
        {
            if (firstAirDate != null && firstAirDate.HasValue)
                return firstAirDate.Value;
            else if (lastAirDate != null && lastAirDate.HasValue)
                return lastAirDate.Value;
            else
                return null;
        }
    }
}
