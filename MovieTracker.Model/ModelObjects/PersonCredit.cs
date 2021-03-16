using System;
using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public abstract class PersonCredit
	{
		public int Id { get; set; }
		public string CreditId { get; set; }
		public string Title { get; set; }
		public string OriginalTitle { get; set; }
		public string Poster { get; set; }
		public DateTime? ReleaseDate { get; set; }
		public bool Adult { get; set; }
	}

	public class PersonCast : PersonCredit
	{
		public string Character { get; set; }
	}

	public class PersonCrew : PersonCredit
	{
		public string Department { get; set; }
		public string Job { get; set; }
	}

	public class PersonCredits
	{
		public IEnumerable<PersonCast> Cast { get; set; }
		public IEnumerable<PersonCrew> Crew { get; set; }
	}
}
