using System;

namespace MovieTracker.Model.ModelObjects
{
	public class Episode : Resource
	{
		public string Name { get; set; }
		public string Overview { get; set; }
		public string Backdrop { get; set; }
		public string ProductionCode { get; set; }
		public DateTime? AirDate { get; set; }
		public int? SeasonNumber { get; set; }
		public int EpisodeNumber { get; set; }
		public MediaCredits Credits { get; set; }
		public Images Images { get; set; }
		public Videos Videos { get; set; }
		public decimal VoteAverage { get; set; }
		public int VoteCount { get; set; }
		public ExternalIds External { get; set; }
	}
}
