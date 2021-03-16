using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public class Certification
	{
		public string Name { get; set; }
		public string Meaning { get; set; }
	}

	class Certifications
	{
		public IEnumerable<Certification> Results { get; set; }
	}
}
