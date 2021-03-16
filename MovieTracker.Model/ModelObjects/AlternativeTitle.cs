using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public class AlternativeTitle
	{
		public string CountryCode { get; set; }
		public string Title { get; set; }
	}

	public class AlternativeTitles
	{
		public IEnumerable<AlternativeTitle> Results { get; set; }
	}
}
