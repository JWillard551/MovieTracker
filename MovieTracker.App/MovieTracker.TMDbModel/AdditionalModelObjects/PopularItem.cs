using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieTracker.TMDbModel.AdditionalModelObjects
{
    public class PopularItem
    {
        public int Id { get; set; }
        public Uri Poster { get; set; }
        public string PopularItemName { get; set; }
        public MediaType MediaType { get; set; }

        public PopularItem(SearchMovie m)
        {
            Id = m.Id;
            MediaType = m.MediaType;
            Poster = Utils.ModelUtils.GetSmallImageUri(m.PosterPath);
            PopularItemName = m.Title;
        }

        public PopularItem(SearchTv s)
        {
            Id = s.Id;
            MediaType = s.MediaType;
            Poster = Utils.ModelUtils.GetSmallImageUri(s.PosterPath);
            PopularItemName = s.Name;
        }
    }
}
