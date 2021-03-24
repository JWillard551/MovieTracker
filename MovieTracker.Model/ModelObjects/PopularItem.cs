using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Model.ModelObjects
{
    public class PopularItem
    {
        public int Id { get; set; }
        public Uri Poster { get; set; }
        public string PopularItemName { get; set; }

        public PopularItem(System.Net.TMDb.Movie m)
        {
            Id = m.Id;
            Poster = Utils.ModelUtils.GetSmallImageUri(m.Poster);
            PopularItemName = m.Title;
        }

        public PopularItem(System.Net.TMDb.Show s)
        {
            Id = s.Id;
            Poster = Utils.ModelUtils.GetSmallImageUri(s.Poster);
            PopularItemName = s.Name;
        }
    }
}
