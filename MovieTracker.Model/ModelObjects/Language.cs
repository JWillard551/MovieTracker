namespace MovieTracker.Model.ModelObjects
{
	public class Language
	{
		public string Code { get; set; }
		public string Name { get; set; }

		public Language(System.Net.TMDb.Language lang)
		{
			Code = lang.Code;
			Name = lang.Name;
		}
	}
}
