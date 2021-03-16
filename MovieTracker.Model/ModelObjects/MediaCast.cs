using System.Collections.Generic;

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

	public class MediaCastList : List<MediaCast>
	{
		public readonly string GroupName = "Cast";

		public MediaCastList(IEnumerable<MediaCast> castList) : base(castList) { }
	}
}
