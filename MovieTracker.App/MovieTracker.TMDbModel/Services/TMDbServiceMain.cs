﻿using MovieTracker.TMDbModel.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.Trending;
using TMDbLib.Objects.TvShows;

namespace MovieTracker.TMDbModel.Services
{
    public partial class TMDbService : ITMDbService
    {
        private readonly string SESSION_ID_TOKEN = "session_id_token";
        private readonly string LANGUAGE_CODE = "en_US";

        public async Task<Movie> GetMovieAsync(int movieId, MovieMethods extraMethods = MovieMethods.Undefined, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.GetMovieAsync(movieId, extraMethods, cancellationToken);
        }

        public async Task<SingleResultContainer<Dictionary<string, WatchProviders>>> GetWatchProvidersAsync(int mediaId, MediaType mediaType, CancellationToken cancellationToken = default)
        {
            if (mediaType == MediaType.Movie)
                return await TMDbServiceClient.Instance.GetMovieWatchProvidersAsync(mediaId, cancellationToken);

            if (mediaType == MediaType.Tv)
                return await TMDbServiceClient.Instance.GetTvShowWatchProvidersAsync(mediaId, cancellationToken);

            return new SingleResultContainer<Dictionary<string, WatchProviders>>();
        }

        public async Task<Person> GetPersonAsync(int personId, PersonMethods extraMethods = PersonMethods.Undefined, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.GetPersonAsync(personId, extraMethods, cancellationToken);
        }

        public async Task<SearchContainer<SearchMovie>> GetPopularMoviesAsync(string language, int page, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.GetMoviePopularListAsync(language, page, null, cancellationToken);
        }

        public async Task<SearchContainer<SearchTv>> GetPopularShowsAsync(string language, int page, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.GetTvShowPopularAsync(page, language, cancellationToken);
        }

        public async Task<SearchContainer<SearchMovie>> GetTrendingMoviesAsync(TimeWindow timeWindow, int page = 0, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.GetTrendingMoviesAsync(timeWindow, page, cancellationToken);
        }

        public async Task<SearchContainer<SearchTv>> GetTrendingTvAsync(TimeWindow timeWindow, int page = 0, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.GetTrendingTvAsync(timeWindow, page, cancellationToken);
        }

        public async Task<TvShow> GetTVShowAsync(int showId, TvShowMethods extraMethods = TvShowMethods.Undefined, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.GetTvShowAsync(showId, extraMethods, null, null, cancellationToken);
        }

        public async Task<SearchContainer<SearchBase>> SearchAsync(string query, int page, CancellationToken cancellationToken = default)
        {
            var results = await TMDbServiceClient.Instance.SearchMultiAsync(query, page, false, 0, null, cancellationToken);
            results.Results = results.Results.Where(x => !(x is SearchPerson)).ToList();
            return results;
        }
    }
}
