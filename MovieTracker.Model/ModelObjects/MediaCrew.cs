using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public class MediaCrew : MediaCredit
	{
		public string Department { get; set; }
		public string Job { get; set; }

		public MediaCrew(System.Net.TMDb.MediaCrew crew) : base(crew)
		{
			Department = crew.Department;
			Job = crew.Job;
		}
	}

	public class MediaCrewList : List<MediaCrew>
	{
		public readonly string GroupName = "Crew";

		public MediaCrewList(IEnumerable<MediaCrew> castList) : base(castList) { }
	}
}
