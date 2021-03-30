using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MovieTracker.Model.ModelObjects
{
	[DataContract]
	public class List
	{
		[DataMember(Name ="id")]
		public string Id { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		[DataMember(Name = "poster_path")]
		public string Poster { get; set; }

		[DataMember(Name = "iso_639_1")]
		public string LanguageCode { get; set; }

		[DataMember(Name = "favorite_count")]
		public int FavoriteCount { get; set; }

		[DataMember(Name = "item_count")]
		public int ItemCount { get; set; }
	}

	public class Lists : PagedResult<List> { }
}
