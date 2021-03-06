using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTracker.Model.ModelObjects
{
	public abstract class Resource
	{
		public int Id { get; set; }
	}

	public class Resources : PagedResult<Resource>
	{
	}

	public class Account
	{
		public int Id { get; set; }

		public bool IncludeAdult { get; set; }

		public string CountryCode { get; set; }

		public string LanguageCode { get; set; }

		public string Name { get; set; }

		public string UserName { get; set; }
	}

	public class ChangedItem
	{
		public int Id { get; set; }

		public bool Adult { get; set; }
	}

	public class Changes : PagedResult<ChangedItem>
	{
	}

	public class Certification
	{
		public string Name { get; set; }

		public string Meaning { get; set; }
	}

	class Certifications
	{
		public IEnumerable<Certification> Results { get; set; }
	}

	public class Collection
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Poster { get; set; }

		public string Backdrop { get; set; }

		public IEnumerable<Movie> Parts { get; set; }

		public Collection(System.Net.TMDb.Collection c)
        {
			Id = c.Id;
			Name = c.Name;
			Poster = c.Poster;
			Backdrop = c.Backdrop;
			Parts = c.Parts?.Select(part => new Movie(part));
        }
	}

	public class Collections : PagedResult<Collection>
	{
	}

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

	public class Companies : PagedResult<Company>
	{
	}

	public class Country
	{
		public string Code { get; set; }

		public string Name { get; set; }

		public Country(System.Net.TMDb.Country country)
        {
			Code = country.Code;
			Name = country.Name;
        }
	}

	public class Genre
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public Genre(System.Net.TMDb.Genre genre)
        {
			Id = genre.Id;
			Name = genre.Name;
        }
	}

	class Jobs
	{
		public IEnumerable<Job> Results { get; set; }
	}

	public class Job
	{
		public string Department { get; set; }

		public IEnumerable<string> Items { get; set; }
	}

	public class Shows : PagedResult<Show>
	{
	}

	public class Show : Resource
	{
		public string Name { get; set; }

		public string OriginalName { get; set; }

		public string Overview { get; set; }

		public string Poster { get; set; }

		public string Backdrop { get; set; }

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

		public MediaCredits Credits { get; set; }

		public Images Images { get; set; }

		public Videos Videos { get; set; }

		public Keywords Keywords { get; set; }

		public Translations Translations { get; set; }

		public decimal Popularity { get; set; }

		public decimal VoteAverage { get; set; }

		public int VoteCount { get; set; }

		public string Status { get; set; }

		public ExternalIds External { get; set; }

		public Show(System.Net.TMDb.Show s)
        {
			Name = s.Name;
			OriginalName = s.OriginalName;
			Overview = s.Overview;
			Poster = s.Poster;
			Backdrop = s.Backdrop;
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
			//Credits
			//Images
			//Videos
			//Keywords
			//Translations
			Popularity = s.Popularity;
			VoteAverage = s.VoteAverage;
			VoteCount = s.VoteCount;
			Status = s.Status;
			//ExternalIds
        }

		public Show() { }
	}

	public class Season : Resource
	{
		public string Name { get; set; }

		public string Overview { get; set; }

		public DateTime? AirDate { get; set; }

		public string Poster { get; set; }

		public int SeasonNumber { get; set; }

		public MediaCredits Credits { get; set; }

		public Images Images { get; set; }

		public Videos Videos { get; set; }

		public IEnumerable<Episode> Episodes { get; set; }

		public ExternalIds External { get; set; }

		public Season(System.Net.TMDb.Season s)
        {
			Name = s.Name;
			Overview = s.Overview;
			AirDate = s.AirDate;
			Poster = s.Poster;
			SeasonNumber = s.SeasonNumber;
			//Credits
			//Images
			//Videos
			//Episodes
			//ExternalIds
        }
	}

	public class Episode : Resource
	{
		public string Name { get; set; }

		public string Overview { get; set; }

		public string Backdrop { get; set; }

		public string ProductionCode { get; set; }

		public DateTime? AirDate { get; set; }

		public int? SeasonNumber { get; set; }

		public int EpisodeNumber { get; set; }

		public MediaCredits Credits { get; set; }

		public Images Images { get; set; }

		public Videos Videos { get; set; }

		public decimal VoteAverage { get; set; }

		public int VoteCount { get; set; }

		public ExternalIds External { get; set; }
	}

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

	public class ExternalIds
	{
		public string Freebase { get; set; }

		public string FreebaseMid { get; set; }

		public string Imdb { get; set; }

		public int? Tvdb { get; set; }

		public int? Tvrage { get; set; }
	}

	class Genres
	{
		public IEnumerable<Genre> Results { get; set; }
	}

	public class Image
	{
		public string FilePath { get; set; }

		public short Width { get; set; }

		public short Height { get; set; }

		public string LanguageCode { get; set; }

		public decimal AspectRatio { get; set; }

		public decimal VoteAverage { get; set; }

		public int VoteCount { get; set; }
	}

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

	public class Movie : Resource
	{
		public string Title { get; set; } = "Uninstantiated Title";

		public string OriginalTitle { get; set; }

		public string TagLine { get; set; }

		public string Overview { get; set; }

		public string Poster { get; set; }

		public string Backdrop { get; set; }

		public bool Adult { get; set; }

		public Collection BelongsTo { get; set; }

		public int Budget { get; set; }

		public IEnumerable<Genre> Genres { get; set; }

		public string HomePage { get; set; }

		public string Imdb { get; set; }

		public IEnumerable<Company> Companies { get; set; }

		public IEnumerable<Country> Countries { get; set; }

		public DateTime? ReleaseDate { get; set; }

		public Int64 Revenue { get; set; }

		public int? Runtime { get; set; }

		public IEnumerable<Language> Languages { get; set; }

		public AlternativeTitles AlternativeTitles { get; set; }

		public MediaCredits Credits { get; set; }

		public Images Images { get; set; }

		public Videos Videos { get; set; }

		public Keywords Keywords { get; set; }

		public Releases Releases { get; set; }

		public Translations Translations { get; set; }

		public decimal Popularity { get; set; }

		public decimal VoteAverage { get; set; }

		public int VoteCount { get; set; }

		public string Status { get; set; }

		public ExternalIds External { get; set; }

		public Movie(System.Net.TMDb.Movie m)
        {
			Title = m.Title;
			OriginalTitle = m.OriginalTitle;
			TagLine = m.TagLine;
			Overview = m.Overview;
			Poster = m.Poster;
			Backdrop = m.Backdrop;
			Adult = m.Adult;
			if (m.BelongsTo != null)
				BelongsTo = new Collection(m.BelongsTo);
			Budget = m.Budget;
			Genres = m.Genres?.Select(genre => new Genre(genre));
			HomePage = m.HomePage;
			Imdb = m.Imdb;
			Companies = m.Companies?.Select(company => new Company() { Id = company.Id, Name = company.Name, Description = company.Description });
			Countries = m.Countries?.Select(country => new Country(country));
			ReleaseDate = m.ReleaseDate;
			Revenue = m.Revenue;
			Runtime = m.Runtime;
			Languages = m.Languages?.Select(lang => new Language(lang));
			//AlternativeTitles
			//Credits
			//Images
			//Videos
			//Keywords
			//Releases
			//Translations
			Popularity = m.Popularity;
			VoteAverage = m.VoteAverage;
			VoteCount = m.VoteCount;
			Status = m.Status;
			//ExternalIds
        }

		public Movie() { }
	}

	public class Movies : PagedResult<Movie>
	{
	}

	public class Images
	{
		public IEnumerable<Image> Backdrops { get; set; }

		public IEnumerable<Image> Posters { get; set; }
	}

	public class AlternativeTitles
	{
		public IEnumerable<AlternativeTitle> Results { get; set; }
	}

	public class AlternativeTitle
	{
		public string CountryCode { get; set; }

		public string Title { get; set; }
	}

	public class MediaCredits
	{
		public IEnumerable<MediaCast> Cast { get; set; }

		public IEnumerable<MediaCrew> Crew { get; set; }
	}

	public abstract class MediaCredit
	{
		public int Id { get; set; }

		public string CreditId { get; set; }

		public string Name { get; set; }

		public string Profile { get; set; }
	}

	public class MediaCast : MediaCredit
	{
		public string Character { get; set; }
	}

	public class MediaCrew : MediaCredit
	{
		public string Department { get; set; }

		public string Job { get; set; }
	}

	public class Keywords
	{
		public IEnumerable<Keyword> Results { get; set; }
	}

	public class Translations
	{
		public IEnumerable<Translation> Results { get; set; }
	}

	public class Releases
	{
		public IEnumerable<Release> Results { get; set; }
	}

	public class Release
	{
		public string CountryCode { get; set; }

		public string Certification { get; set; }

		public DateTime Date { get; set; }
	}

	public class Videos
	{
		public IEnumerable<Video> Results { get; set; }
	}

	public class Video
	{
		public string Id { get; set; }

		public string LanguageCode { get; set; }

		public string Key { get; set; }

		public string Site { get; set; }

		public int Size { get; set; }

		public string Type { get; set; }
	}

	public class Translation
	{
		public string LanguageCode { get; set; }

		public string Name { get; set; }

		public string EnglishName { get; set; }
	}

	public class Keyword
	{
		public int Id { get; set; }

		public string Name { get; set; }
	}

	public class Person : Resource
	{
		public string Name { get; set; }

		public bool Adult { get; set; }

		public IEnumerable<string> KnownAs { get; set; }

		public string Biography { get; set; }

		public string BirthDay { get; set; }

		public string DeathDay { get; set; }

		public string HomePage { get; set; }

		public string BirthPlace { get; set; }

		public string Poster { get; set; }

		public PersonCredits Credits { get; set; }

		public PersonImages Images { get; set; }

		public ExternalIds External { get; set; }

		public Person(System.Net.TMDb.Person p)
        {
			Name = p.Name;
			Adult = p.Adult;
			KnownAs = p.KnownAs;
			Biography = p.Biography;
			BirthDay = p.BirthDay;
			DeathDay = p.DeathDay;
			HomePage = p.HomePage;
			BirthPlace = p.BirthPlace;
			Poster = p.Poster;
			//PersonCredits
			//PersonImages
			//ExternalIds
        }
	}

	public class PersonImages
	{
		public IEnumerable<Image> Results { get; set; }
	}

	public class PersonCredits
	{
		public IEnumerable<PersonCast> Cast { get; set; }

		public IEnumerable<PersonCrew> Crew { get; set; }
	}

	public abstract class PersonCredit
	{
		public int Id { get; set; }

		public string CreditId { get; set; }

		public string Title { get; set; }

		public string OriginalTitle { get; set; }

		public string Poster { get; set; }

		public DateTime? ReleaseDate { get; set; }

		public bool Adult { get; set; }
	}

	public class PersonCast : PersonCredit
	{
		public string Character { get; set; }
	}

	public class PersonCrew : PersonCredit
	{
		public string Department { get; set; }

		public string Job { get; set; }
	}

	public class People : PagedResult<Person>
	{
	}

	public class Review
	{
		public string Id { get; set; }

		public string Author { get; set; }

		public string Content { get; set; }

		public string LanguageCode { get; set; }

		public int MediaId { get; set; }

		public string MediaTitle { get; set; }

		public string MediaType { get; set; }

		public string Url { get; set; }
	}

	public class Reviews : PagedResult<Review>
	{
	}

	public class List
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string Poster { get; set; }

		public string Creator { get; set; }

		public string LanguageCode { get; set; }

		public IEnumerable<Movie> Movies { get; set; }

		public int FavoriteCount { get; set; }

		public int MovieCount { get; set; }
	}

	public class Lists : PagedResult<List>
	{
	}

	public abstract class PagedResult<T>
	{
		public IEnumerable<T> Results { get; set; }

		public int PageIndex { get; set; }

		public int PageCount { get; set; }

		public int TotalCount { get; set; }
	}

	class ResourceFindResult
	{
		public IEnumerable<Movie> Movies { get; set; }

		public IEnumerable<Person> People { get; set; }

		public IEnumerable<Show> Shows { get; set; }

		public IEnumerable<Season> Seasons { get; set; }

		public IEnumerable<Episode> Episodes { get; set; }
	}

	class AuthenticationResult
	{
		public string Token { get; set; }

		public string Session { get; set; }

		public string Guest { get; set; }

		public DateTime? ExpiresAt { get; set; }

		public bool Success { get; set; }
	}
}
