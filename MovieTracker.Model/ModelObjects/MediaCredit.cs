using MovieTracker.Model.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTracker.Model.ModelObjects
{
	public class MediaCredit
	{
		public int Id { get; set; }
		public string CreditId { get; set; }
		public string Name { get; set; }
		public Uri Profile { get; set; }

		//Crew Properties
		public string Department { get; set; }
		public string Job { get; set; }

		//Cast Properties
		public string Character { get; set; }

		public MediaCredit(System.Net.TMDb.MediaCredit credit)
		{
			Id = credit.Id;
			CreditId = credit.CreditId;
			Name = credit.Name;
			Profile = ModelUtils.GetSmallImageUri(credit.Profile);
			Department = string.Empty;
			Job = string.Empty;
			Character = string.Empty;
		}

		public MediaCredit(System.Net.TMDb.MediaCrew crewCredit)
        {
			Id = crewCredit.Id;
			CreditId = crewCredit.CreditId;
			Name = crewCredit.Name;
			Profile = ModelUtils.GetSmallImageUri(crewCredit.Profile);
			Department = crewCredit.Department;
			Job = crewCredit.Job;
			Character = string.Empty;
		}

		public MediaCredit(System.Net.TMDb.MediaCast castCredit)
		{
			Id = castCredit.Id;
			CreditId = castCredit.CreditId;
			Name = castCredit.Name;
			Profile = ModelUtils.GetSmallImageUri(castCredit.Profile);
			Department = string.Empty;
			Job = string.Empty;
			Character = castCredit.Character;
		}
	}

	public class MediaCredits : List<MediaCredit>
    {
		public string GroupName { get; set; }

		public MediaCredits(string groupName, IEnumerable<MediaCredit> credits) : base(credits)
        {
			GroupName = groupName;
        }
    }
}
