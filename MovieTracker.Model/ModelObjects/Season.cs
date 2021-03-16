using System;
using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public class Season : Resource
	{
		public string Name { get; set; }
		public string Overview { get; set; }
		public DateTime? AirDate { get; set; }
		public string Poster { get; set; }
		public int SeasonNumber { get; set; }
		public MediaCredits Credits { get; set; }
		public Images Images { get; set; }
		public Videos Videos { get; set; }
		public IEnumerable<Episode> Episodes { get; set; }
		public ExternalIds External { get; set; }

		public Season(System.Net.TMDb.Season s)
		{
			Name = s.Name;
			Overview = s.Overview;
			AirDate = s.AirDate;
			Poster = s.Poster;
			SeasonNumber = s.SeasonNumber;
			//Credits
			//Images
			//Videos
			//Episodes
			//ExternalIds
		}
	}
}
