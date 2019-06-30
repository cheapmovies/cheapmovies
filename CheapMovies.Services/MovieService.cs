using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using CheapMovies.Services.Configuration;
using CheapMovies.Domain.Entities;
using System;

namespace CheapMovies.Services
{
    public class MovieService : IMovieService
    {
        private IConfiguration configuration { get; set; }
        private IMovieDataService movieDataService { get; set; }
        private IStoreRepository storeRepository { get; set; }
        private Provider[] providers { get; set; }

        public MovieService(
            IConfiguration configuration,
            IMovieDataService movieDataService,
            IStoreRepository storeRepository)
        {
            this.movieDataService = movieDataService;
            this.storeRepository = storeRepository;
            var providerSettings = new ProviderSettings();

            if (configuration != null)
            {
                this.configuration = configuration;
                this.configuration.Bind(nameof(ProviderSettings), providerSettings);
                this.providers = providerSettings.Providers;
            }
        }

        public List<string> GetProviders()
        {
            var result = new List<string>();
            foreach (Provider provider in this.providers)
            {
                result.Add(provider.Name);
            }

            return result;
        }

        public async Task<List<Movie>> GetMoviesAsync(int serviceId)
        {
            string result = await this.movieDataService.GetMoviesAsync(serviceId);
            bool fromStore = false;
            this.UseStore(serviceId.ToString(), ref result, ref fromStore);
            return this.ParseMovies(result);
        }

        public List<Movie> ParseMovies(string json)
        {
            List<Movie> output = new List<Movie>();

            var moviesJson = JObject.Parse(json);
            JArray movies = (JArray)moviesJson["Movies"];

            foreach (var movie in movies)
            {
                output.Add(new Movie((JObject)movie));
            }

            return output;
        }

        public async Task<Movie> GetMovieAsync(int serviceId, string movieId)
        {
            string result = await this.movieDataService.GetMovieAsync(serviceId, movieId);
            bool fromStore = false;
            this.UseStore(serviceId.ToString() + ":" + movieId, ref result, ref fromStore);
            var movie = new Movie(result);
            movie.FromStore = fromStore;
            return movie;
        }

        private void UseStore(string key, ref string value, ref bool fromStore)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = this.storeRepository.GetValue(key);
                fromStore = true;
            }
            else
            {
                this.storeRepository.StoreValue(key, value);
                fromStore = false;
            }
        }
    }
}