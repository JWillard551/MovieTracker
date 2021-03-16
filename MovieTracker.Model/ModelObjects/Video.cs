using System.Collections.Generic;


namespace MovieTracker.Model.ModelObjects
{
	public class Video
	{
		public string Id { get; set; }
		public string LanguageCode { get; set; }
		public string Key { get; set; }
		public string Site { get; set; }
		public int Size { get; set; }
		public string Type { get; set; }
	}

	public class Videos
	{
		public IEnumerable<Video> Results { get; set; }
	}
}
