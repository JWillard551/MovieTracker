namespace MovieTracker.Model.ModelObjects
{
	public class Company
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string HeadQuarters { get; set; }
		public string HomePage { get; set; }
		public Company Parent { get; set; }
		public string Logo { get; set; }
	}

	public class Companies : PagedResult<Company> { }
}
