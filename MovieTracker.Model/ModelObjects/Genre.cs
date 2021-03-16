using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public class Genre
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public Genre(System.Net.TMDb.Genre genre)
		{
			Id = genre.Id;
			Name = genre.Name;
		}
	}

	public class Genres
	{
		public IEnumerable<Genre> Results { get; set; }
	}
}
