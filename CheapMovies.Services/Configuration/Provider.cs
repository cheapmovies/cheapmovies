using System;
using System.Net.Http;
using System.Net.Http.Headers;
using CheapMovies.Common;

namespace CheapMovies.Services.Configuration
{
    public class Provider
    {
        public string Name { get; set; }
        public string BaseAddress { get; set; }
        public string MoviesService { get; set; }
        public string MovieService { get; set; }
        public string Token { get; set; }
        public string Prefix { get; set; }
        public HttpClient HttpClient {
            get
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(this.BaseAddress);
                client.DefaultRequestHeaders.Add(Constants.HTTP_HEADER_ACCESS_TOKEN, this.Token);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue(Constants.HTTP_MEDIA_TYPE_JSON));

                return client;
            }
        }
    }
}