namespace MovieTracker.Model.ModelObjects
{
	public class Country
	{
		public string Code { get; set; }
		public string Name { get; set; }

		public Country(System.Net.TMDb.Country country)
		{
			Code = country.Code;
			Name = country.Name;
		}
	}
}
