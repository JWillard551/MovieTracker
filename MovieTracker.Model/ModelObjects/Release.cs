using System;
using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public class Release
	{
		public string CountryCode { get; set; }
		public string Certification { get; set; }
		public DateTime Date { get; set; }

		public Release(System.Net.TMDb.Release r)
		{
			CountryCode = r.CountryCode;
			Certification = r.Certification;
			Date = r.Date;
		}
	}

	public class Releases
	{
		public IEnumerable<Release> Results { get; set; }
	}
}
