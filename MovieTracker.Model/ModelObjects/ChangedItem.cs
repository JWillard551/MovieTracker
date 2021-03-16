namespace MovieTracker.Model.ModelObjects
{
	public class ChangedItem
	{
		public int Id { get; set; }
		public bool Adult { get; set; }
	}

	public class ChangeItems : PagedResult<ChangedItem> { }
}
