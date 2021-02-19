using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Model.ModelObjects
{
    public class SearchResult
    {
        public Genre Genre { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Overview { get; set; }
        public decimal Rating { get; set; }
        
        public SearchResult() { }

        public SearchResult(System.Net.TMDb.Resource resource) 
        {
            if (resource is System.Net.TMDb.Movie)
            {
                var o = resource as System.Net.TMDb.Movie;
                Title = o.OriginalTitle;
                ReleaseDate = o.ReleaseDate.Value;
                Overview = o.Overview;
                Rating = o.VoteAverage * 10;
            }
            else if (resource is System.Net.TMDb.Show)
            {
                var o = resource as System.Net.TMDb.Show;
                Title = o.OriginalName;
                ReleaseDate = o.FirstAirDate.Value;
                Overview = o.Overview;
                Rating = o.VoteAverage * 10;
            }
            else if (resource is System.Net.TMDb.Person)
            {
                var o = resource as System.Net.TMDb.Person;
                Title = o.Name;
                ReleaseDate = DateTime.Now; //TODO:
                Overview = o.Biography;
                Rating = 0;
            }
            else
            {
                Title = "Unknown Object";
                ReleaseDate = DateTime.Now; //TODO:
                Overview = "Unknown Object";
                Rating = 0;
            }
        }
    }
}
