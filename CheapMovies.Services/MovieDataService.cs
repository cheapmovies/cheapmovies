using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CheapMovies.Common;
using CheapMovies.Common.Utilities;
using CheapMovies.Services.Configuration;

namespace CheapMovies.Services
{
    public class MovieDataService : IMovieDataService
    {
        private IConfiguration configuration { get; set; }
        private Provider[] providers { get; set; }
        private int maxRetryAttempts = 3;
        private int timeoutSeconds = 10;
        private TimeSpan pauseBetweenFailures = TimeSpan.FromSeconds(1);

        public MovieDataService(IConfiguration configuration)
        {
            this.configuration = configuration;

            this.maxRetryAttempts = this.configuration.GetValue(Constants.CONFIG_RETRY_MAX_ATTEMPTS, 3);
            var seconds = this.configuration.GetValue(Constants.CONFIG_RETRY_SECONDS_PAUSE_BETWEEN_FAILURES, 1);
            this.pauseBetweenFailures = TimeSpan.FromSeconds(seconds);
            this.timeoutSeconds = this.configuration.GetValue(Constants.CONFIG_TIMEOUT_SECONDS, 10);

            var providerSettings = new ProviderSettings();
            this.configuration.Bind(nameof(ProviderSettings), providerSettings);
            this.providers = providerSettings.Providers;
        }

        public async Task<string> GetMoviesAsync(int serviceId)
        {
            return await RetryHelper.RetryOnExceptionAsync(
                this.maxRetryAttempts, this.pauseBetweenFailures, async () =>
                    await this.GetMoviesOperationAsync(serviceId)
            ); 
        }

        public async Task<string> GetMoviesOperationAsync(int serviceId)
        {
            Provider provider = this.providers[serviceId];
            var client = provider.HttpClient;
            client.Timeout = TimeSpan.FromSeconds(this.timeoutSeconds);
            var result = await client.GetStringAsync(provider.MoviesService);
            return result;
        }

        public async Task<string> GetMovieAsync(int serviceId, string movieId)
        {
            return await RetryHelper.RetryOnExceptionAsync(
                this.maxRetryAttempts, this.pauseBetweenFailures, async () =>
                    await this.GetMovieOperationAsync(serviceId, movieId)
            ); 
        }

        public async Task<string> GetMovieOperationAsync(int serviceId, string movieId)
        {
            Provider provider = this.providers[serviceId];
            var client = provider.HttpClient;
            client.Timeout = TimeSpan.FromSeconds(this.timeoutSeconds);
            var result = await client.GetStringAsync(provider.MovieService + "/" + provider.Prefix + movieId);
            return result;
        }
    }
}