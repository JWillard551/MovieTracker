using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public class Keyword
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class Keywords
	{
		public IEnumerable<Keyword> Results { get; set; }
	}
}
