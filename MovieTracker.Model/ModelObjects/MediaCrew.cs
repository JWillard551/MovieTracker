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
}
