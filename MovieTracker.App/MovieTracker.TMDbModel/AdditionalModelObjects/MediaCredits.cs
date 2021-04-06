using MovieTracker.TMDbModel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace MovieTracker.TMDbModel.AdditionalModelObjects
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

		public MediaCredit(Credits credit)
		{
			Id = credit.Id;
		}

		public MediaCredit(Crew crew)
		{
			Id = crew.Id;
			CreditId = crew.CreditId;
			Name = crew.Name;
			Profile = ModelUtils.GetSmallImageUri(crew.ProfilePath);
			Department = crew.Department;
			Job = crew.Job;
			Character = string.Empty;
		}

		public MediaCredit(Cast cast)
		{
			Id = cast.Id;
			CreditId = cast.CreditId;
			Name = cast.Name;
			Profile = ModelUtils.GetSmallImageUri(cast.ProfilePath);
			Department = string.Empty;
			Job = string.Empty;
			Character = cast.Character;
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
