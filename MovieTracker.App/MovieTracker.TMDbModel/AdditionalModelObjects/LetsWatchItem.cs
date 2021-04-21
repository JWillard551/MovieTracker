using System;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieTracker.TMDbModel.AdditionalModelObjects
{
    public class LetsWatchItem
    {
        public int Id { get; set; }
        public Uri Poster { get; set; }
        public string Name { get; set; }
        public MediaType MediaType { get; set; }

        public bool Rated { get; set; }

        public bool Favorite { get; set; }

        public bool Watchlist { get; set; }

        public LetsWatchItem(SearchMovie m, bool rated = false, bool favorite = false, bool watchlist = false)
        {
            Id = m.Id;
            MediaType = m.MediaType;
            Poster = Utils.ModelUtils.GetSmallImageUri(m.PosterPath);
            Name = m.Title;
            Rated = rated;
            Favorite = favorite;
            Watchlist = watchlist;
        }

        public LetsWatchItem(SearchTv s, bool rated = false, bool favorite = false, bool watchlist = false)
        {
            Id = s.Id;
            MediaType = s.MediaType;
            Poster = Utils.ModelUtils.GetSmallImageUri(s.PosterPath);
            Name = s.Name;
            Rated = rated;
            Favorite = favorite;
            Watchlist = watchlist;
        }
    }
}
