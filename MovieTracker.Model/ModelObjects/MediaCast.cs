namespace MovieTracker.Model.ModelObjects
{
	public class MediaCast : MediaCredit
	{
		public string Character { get; set; }

		public MediaCast(System.Net.TMDb.MediaCast cast) : base(cast)
		{
			Character = cast.Character;
		}
	}
}
