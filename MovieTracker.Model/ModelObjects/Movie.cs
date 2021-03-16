using MovieTracker.Model.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTracker.Model.ModelObjects
{
	public class Movie : Resource
	{
		public string Title { get; set; }
		public string OriginalTitle { get; set; }
		public string TagLine { get; set; }
		public string Overview { get; set; }
		public Uri Poster { get; set; }
		public Uri Backdrop { get; set; }
		public bool Adult { get; set; }
		public Collection BelongsTo { get; set; }
		public string Budget { get; set; }
		public string Genres { get; set; }
		public string HomePage { get; set; }
		public string Imdb { get; set; }
		public IEnumerable<Company> Companies { get; set; }
		public IEnumerable<Country> Countries { get; set; }
		public DateTime? ReleaseDate { get; set; }
		public string Revenue { get; set; }
		public int? Runtime { get; set; }
		public IEnumerable<Language> Languages { get; set; }
		public AlternativeTitles AlternativeTitles { get; set; }
		public List<MediaCredits> Credits { get; set; }
		public Images Images { get; set; }
		public Videos Videos { get; set; }
		public Keywords Keywords { get; set; }
		public IEnumerable<Release> Releases { get; set; }
		public string Rating
		{
			get
			{
				if (Releases != null && Releases.Any())
					return Releases.Where(r => r.CountryCode.Equals("US")).FirstOrDefault()?.Certification ?? "??";
				else
					return "??";
			}
		}
		public Translations Translations { get; set; }
		public decimal Popularity { get; set; }
		public decimal VoteAverage { get; set; }
		public int VoteCount { get; set; }
		public string Status { get; set; }
		public ExternalIds External { get; set; }

		public Movie(System.Net.TMDb.Movie m, IEnumerable<System.Net.TMDb.Release> releases = null)
		{
			Title = m.Title;
			OriginalTitle = m.OriginalTitle;
			TagLine = m.TagLine;
			Overview = m.Overview;
			Poster = ModelUtils.GetImageUri(m.Poster);
			Backdrop = ModelUtils.GetImageUri(m.Backdrop);
			Adult = m.Adult;
			if (m.BelongsTo != null)
				BelongsTo = new Collection(m.BelongsTo);
			Budget = m.Budget.ToString();
			Genres = String.Join(", ", m.Genres?.Select(genre => genre.Name).ToList());
			HomePage = m.HomePage;
			Imdb = m.Imdb;
			Companies = m.Companies?.Select(company => new Company() { Id = company.Id, Name = company.Name, Description = company.Description });
			Countries = m.Countries?.Select(country => new Country(country));
			ReleaseDate = m.ReleaseDate;
			Revenue = m.Revenue.ToString();
			Runtime = m.Runtime;
			Languages = m.Languages?.Select(lang => new Language(lang));
			//AlternativeTitles
			//Credits = Utils.ModelUtils.GetMovieMediaCredits(m.Credits);
			//Images
			//Videos
			//Keywords
			Releases = releases?.Select(r => new Release(r));
			//Translations
			Popularity = m.Popularity;
			VoteAverage = m.VoteAverage;
			VoteCount = m.VoteCount;
			Status = m.Status;
			//ExternalIds
		}

		public Movie() { }
	}

	public class Movies : PagedResult<Movie> { }
}
