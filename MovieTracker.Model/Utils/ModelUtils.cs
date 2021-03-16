using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTracker.Model.Utils
{
    public static class ModelUtils
    {
        public static Uri GetImageUri(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return null;
            return new Uri(String.Concat("https://image.tmdb.org/t/p/original", imagePath));
        }

        public static Uri GetSmallImageUri(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return null;
            return new Uri(String.Concat("https://image.tmdb.org/t/p/w500", imagePath));
        }

        public static List<MediaCredits> GetMovieMediaCredits(System.Net.TMDb.Movie movie)
        {
            List<MediaCredits> results = new List<MediaCredits>();

            if (movie.Credits?.Cast?.Any() ?? false)
                results.Add(new MediaCredits("Cast", movie.Credits.Cast.Select(cast => new MediaCredit(cast))));

            if (movie.Credits?.Crew?.Any() ?? false)
                results.Add(new MediaCredits("Crew", movie.Credits.Crew.Select(crew => new MediaCredit(crew)).OrderBy(crew => crew.Department)));

            return results;
        }
    }
}
