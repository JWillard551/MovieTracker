namespace MovieTracker.Model.ModelObjects
{
	public class Network
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public Network(System.Net.TMDb.Network n)
		{
			Id = n.Id;
			Name = n.Name;
		}
	}
}
