using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.TMDb;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.Model
{
    public class Class1
    {

		public static async Task Sample(CancellationToken cancellationToken)
		{
			using (var client = new ServiceClient("bbca921a0b03d5111e60a4010cb91102"))
			{
				var movies = await client.Movies.GetTopRatedAsync(null, 1, cancellationToken);
				var count = movies.PageCount;

				//for (int i = 1, count = 1000; i <= count; i++)
				//{
				//	 // keep track of the actual page count

				//	//foreach (Movie m in movies.Results)
				//	//{
				//	//	var movie = await client.Movies.GetAsync(m.Id, null, true, cancellationToken);

				//	//	var personIds = movie.Credits.Cast.Select(s => s.Id)
				//	//		.Union(movie.Credits.Crew.Select(s => s.Id));

				//	//	foreach (var id in personIds)
				//	//	{
				//	//		var person = await client.People.GetAsync(id, true, cancellationToken);

				//	//		foreach (var img in person.Images.Results)
				//	//		{
				//	//			string filepath = Path.Combine("People", img.FilePath.TrimStart('/'));
				//	//			await DownloadImage(img.FilePath, filepath, cancellationToken);
				//	//		}
				//	//	}
				//	//}
				//}
			}
		}
	}
}
