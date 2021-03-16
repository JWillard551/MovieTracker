using System.Collections.Generic;

namespace MovieTracker.Model.ModelObjects
{
	public abstract class PagedResult<T>
	{
		public IEnumerable<T> Results { get; set; }
		public int PageIndex { get; set; }
		public int PageCount { get; set; }
		public int TotalCount { get; set; }
	}
}
