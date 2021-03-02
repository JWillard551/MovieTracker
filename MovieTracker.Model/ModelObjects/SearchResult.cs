using MovieTracker.Model.ModelEnums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieTracker.Model.ModelObjects
{
    public class SearchResult
    {
        private int Id { get; set; }
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
            ReleaseDate = movie.ReleaseDate.Value;
            Overview = movie.Overview;
            Rating = CalculateRating(movie.VoteAverage);
            Genres = movie.Genres?.Select(genre => new ModelObjects.Genre() { Id = genre.Id, Name = genre.Name }).ToList();
            MediaType = MediaType.Movie;
            ImageUri = GetImageUri(movie.Poster ?? string.Empty);
        }

        public SearchResult(System.Net.TMDb.Show show)
        {
            Id = show.Id;
            ResultName = show.OriginalName;
            ReleaseDate = DetermineShowReleaseDate(show.FirstAirDate, show.LastAirDate);
            Overview = show.Overview;
            Rating = CalculateRating(show.VoteAverage);
            MediaType = MediaType.Show;
            ImageUri = GetImageUri(show.Poster ?? string.Empty);
        }

        public SearchResult(System.Net.TMDb.Person person)
        {
            Id = person.Id;
            ResultName = person.Name;
            Overview = person.Biography;
            MediaType = MediaType.Person;
            ImageUri = GetImageUri(person.Poster ?? string.Empty);
        }

        protected Uri GetImageUri(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return null;
            return new Uri(String.Concat("https://image.tmdb.org/t/p/original", imagePath));
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
