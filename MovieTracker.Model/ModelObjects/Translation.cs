using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public class Translation
	{
		public string LanguageCode { get; set; }
		public string Name { get; set; }
		public string EnglishName { get; set; }
	}

	public class Translations
	{
		public IEnumerable<Translation> Results { get; set; }
	}
}
