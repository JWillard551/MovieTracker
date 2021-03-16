using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public class Image
	{
		public string FilePath { get; set; }
		public short Width { get; set; }
		public short Height { get; set; }
		public string LanguageCode { get; set; }
		public decimal AspectRatio { get; set; }
		public decimal VoteAverage { get; set; }
		public int VoteCount { get; set; }
	}

	public class Images
	{
		public IEnumerable<Image> Backdrops { get; set; }
		public IEnumerable<Image> Posters { get; set; }
	}
}
