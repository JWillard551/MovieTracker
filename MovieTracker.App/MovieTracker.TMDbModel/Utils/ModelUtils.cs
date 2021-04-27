using MovieTracker.TMDbModel.AdditionalModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTracker.TMDbModel.Utils
{
    public static class ModelUtils
    {
        public static int Randomize(int max)
        {
            return new Random().Next(max);
        }

        public static int RandomizeNonFirst(int max)
        {
            var result = new Random().Next(max);
            if (result == 0 && max > 1)
                return result + 1;
            else
                return result;
        }

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

        public static List<MediaCredits> GetMovieMediaCredits(TMDbLib.Objects.Movies.Credits credits)
        {
            List<MediaCredits> results = new List<MediaCredits>();

            if (credits?.Cast?.Any() ?? false)
                results.Add(new MediaCredits("Cast", credits.Cast.Select(cast => new MediaCredit(cast))));

            if (credits?.Crew?.Any() ?? false)
                results.Add(new MediaCredits("Crew", credits.Crew.Select(crew => new MediaCredit(crew)).OrderBy(crew => crew.Department)));

            return results;
        }

        public static List<MediaCredits> GetTvShowMediaCredits(TMDbLib.Objects.TvShows.Credits credits)
        {
            List<MediaCredits> results = new List<MediaCredits>();

            if (credits?.Cast?.Any() ?? false)
                results.Add(new MediaCredits("Cast", credits.Cast.Select(cast => new MediaCredit(cast))));

            if (credits?.Crew?.Any() ?? false)
                results.Add(new MediaCredits("Crew", credits.Crew.Select(crew => new MediaCredit(crew)).OrderBy(crew => crew.Department)));

            return results;
        }
    }
}
