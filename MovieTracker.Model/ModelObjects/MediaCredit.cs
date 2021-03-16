using MovieTracker.Model.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTracker.Model.ModelObjects
{
	public abstract class MediaCredit
	{
		public int Id { get; set; }
		public string CreditId { get; set; }
		public string Name { get; set; }
		public Uri Profile { get; set; }

		public MediaCredit(System.Net.TMDb.MediaCredit credit)
		{
			Id = credit.Id;
			CreditId = credit.CreditId;
			Name = credit.Name;
			Profile = ModelUtils.GetImageUri(credit.Profile);
		}
	}

	public class MediaCredits
	{
		public IEnumerable<MediaCast> Cast { get; set; }
		public IEnumerable<MediaCrew> Crew { get; set; }

		public MediaCredits(System.Net.TMDb.MediaCredits credits)
		{
			Cast = credits.Cast?.Select(cast => new MediaCast(cast)) ?? new List<MediaCast>();
			Crew = credits.Crew?.Select(crew => new MediaCrew(crew)) ?? new List<MediaCrew>();
		}
	}
}
