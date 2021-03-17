using MovieTracker.Model.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTracker.Model.ModelObjects
{
	public class Show : Resource
	{
		public string Name { get; set; }
		public string OriginalName { get; set; }
		public string Overview { get; set; }
		public Uri Poster { get; set; }
		public Uri Backdrop { get; set; }
		public IEnumerable<string> Countries { get; set; }
		public IEnumerable<int> EpisodeRuntimes { get; set; }
		public IEnumerable<Person> CreatedBy { get; set; }
		public DateTime? FirstAirDate { get; set; }
		public DateTime? LastAirDate { get; set; }
		public IEnumerable<Genre> Genres { get; set; }
		public string HomePage { get; set; }
		public bool InProduction { get; set; }
		public int EpisodeCount { get; set; }
		public int SeasonCount { get; set; }
		public IEnumerable<Season> Seasons { get; set; }
		public IEnumerable<string> Languages { get; set; }
		public IEnumerable<Network> Networks { get; set; }
		public decimal Popularity { get; set; }
		public decimal VoteAverage { get; set; }
		public int VoteCount { get; set; }
		public string Status { get; set; }

		public Show(System.Net.TMDb.Show s)
		{
			Name = s.Name;
			OriginalName = s.OriginalName;
			Overview = s.Overview;
			Poster = ModelUtils.GetSmallImageUri(s.Poster);
			Backdrop = ModelUtils.GetSmallImageUri(s.Backdrop);
			Countries = s.Countries;
			EpisodeRuntimes = s.EpisodeRuntimes;
			CreatedBy = s.CreatedBy?.Select(person => new Person(person));
			FirstAirDate = s.FirstAirDate;
			LastAirDate = s.LastAirDate;
			Genres = s.Genres?.Select(genre => new Genre(genre));
			HomePage = s.HomePage;
			InProduction = s.InProduction;
			EpisodeCount = s.EpisodeCount;
			SeasonCount = s.SeasonCount;
			Seasons = s.Seasons?.Select(season => new Season(season));
			Languages = s.Languages;
			Networks = s.Networks?.Select(network => new Network(network));
			Popularity = s.Popularity;
			VoteAverage = s.VoteAverage;
			VoteCount = s.VoteCount;
			Status = s.Status;
		}

		public Show() { }
	}

	public class Shows : PagedResult<Show> { }
}
