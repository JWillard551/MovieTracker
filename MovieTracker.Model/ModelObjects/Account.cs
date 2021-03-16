namespace MovieTracker.Model.ModelObjects
{
	public class Account
	{
		public int Id { get; set; }
		public bool IncludeAdult { get; set; }
		public string CountryCode { get; set; }
		public string LanguageCode { get; set; }
		public string Name { get; set; }
		public string UserName { get; set; }
	}
}
