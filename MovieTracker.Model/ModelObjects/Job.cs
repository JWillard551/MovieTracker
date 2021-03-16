using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public class Job
	{
		public string Department { get; set; }
		public IEnumerable<string> Items { get; set; }
	}

	public class Jobs
	{
		public IEnumerable<Job> Results { get; set; }
	}
}
