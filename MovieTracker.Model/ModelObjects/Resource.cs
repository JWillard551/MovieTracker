using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public abstract class Resource
	{
		public int Id { get; set; }
	}

	public class ResourceFindResult
	{
		public IEnumerable<Movie> Movies { get; set; }
		public IEnumerable<Person> People { get; set; }
		public IEnumerable<Show> Shows { get; set; }
		public IEnumerable<Season> Seasons { get; set; }
		public IEnumerable<Episode> Episodes { get; set; }
	}

	public class Resources : PagedResult<Resource> { }
}
