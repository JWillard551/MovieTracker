using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public class Person : Resource
	{
		public string Name { get; set; }
		public bool Adult { get; set; }
		public IEnumerable<string> KnownAs { get; set; }
		public string Biography { get; set; }
		public string BirthDay { get; set; }
		public string DeathDay { get; set; }
		public string HomePage { get; set; }
		public string BirthPlace { get; set; }
		public string Poster { get; set; }
		public PersonCredits Credits { get; set; }
		public PersonImages Images { get; set; }
		public ExternalIds External { get; set; }

		public Person(System.Net.TMDb.Person p)
		{
			Name = p.Name;
			Adult = p.Adult;
			KnownAs = p.KnownAs;
			Biography = p.Biography;
			BirthDay = p.BirthDay;
			DeathDay = p.DeathDay;
			HomePage = p.HomePage;
			BirthPlace = p.BirthPlace;
			Poster = p.Poster;
			//PersonCredits
			//PersonImages
			//ExternalIds
		}

		public Person() { }
	}

	public class People : PagedResult<Person> { }

	public class PersonImages
	{
		public IEnumerable<Image> Results { get; set; }
	}
}
