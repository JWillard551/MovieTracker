using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public class List
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Poster { get; set; }
		public string Creator { get; set; }
		public string LanguageCode { get; set; }
		public IEnumerable<Movie> Movies { get; set; }
		public int FavoriteCount { get; set; }
		public int MovieCount { get; set; }
	}

	public class Lists : PagedResult<List> { }
}
