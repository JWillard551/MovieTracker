using System.Collections.Generic;
using System.Linq;

namespace MovieTracker.Model.ModelObjects
{
	public class Collection
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Poster { get; set; }
		public string Backdrop { get; set; }
		public IEnumerable<Movie> Parts { get; set; }

		public Collection(System.Net.TMDb.Collection c)
		{
			Id = c.Id;
			Name = c.Name;
			Poster = c.Poster;
			Backdrop = c.Backdrop;
			Parts = c.Parts?.Select(part => new Movie(part));
		}
	}

	public class Collections : PagedResult<Collection> { }
}
