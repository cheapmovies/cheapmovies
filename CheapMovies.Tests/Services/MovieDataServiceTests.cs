using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using CheapMovies.Common;
using CheapMovies.Services;
using System;

namespace CheapMovies.Tests.Services
{
    public class MovieDataServiceTests 
    {
        private Mock<IConfiguration> mockConfiguration;
        private Mock<IMovieDataService> goodMockMovieDataService;
        private Mock<IMovieDataService> badMockMovieDataService;
        private Mock<IStoreRepository> mockStoreRepository;

        public MovieDataServiceTests()
        {        
            this.mockStoreRepository = new Mock<IStoreRepository>();
        }

        [Fact]
        public void MoviesDataServiceMoviesHasDataTest()
        {
            this.goodMockMovieDataService = new Mock<IMovieDataService>();
            this.goodMockMovieDataService.Setup(m => m.GetMoviesAsync(0)).
                Returns(Task.FromResult(this.moviesJsonString));

            IMovieService movieService = new MovieService(
                null,
                this.goodMockMovieDataService.Object, 
                this.mockStoreRepository.Object);
            var movies = movieService.GetMoviesAsync(0).Result;
            
            Assert.Single(movies);
            Assert.Equal("0076759", movies[0].Id);
            mockStoreRepository.Verify(m => m.StoreValue("0", this.moviesJsonString), Times.Once());
        }

        [Fact]
        public void MoviesDataServiceMoviesErrorTest()
        {
            this.badMockMovieDataService = new Mock<IMovieDataService>();
            this.badMockMovieDataService.Setup(m => m.GetMoviesAsync(0)).
                Returns(Task.FromResult(string.Empty));

            this.mockStoreRepository = new Mock<IStoreRepository>();
            this.mockStoreRepository.Setup(m => m.GetValue("0")).
                Returns(this.moviesJsonString);

            IMovieService movieService = new MovieService(
                null,
                this.badMockMovieDataService.Object, 
                this.mockStoreRepository.Object);
            var movies = movieService.GetMoviesAsync(0).Result;

            Assert.Single(movies);
            Assert.Equal("0076759", movies[0].Id);
            mockStoreRepository.Verify(m => m.GetValue("0"), Times.Once());
        }

        [Fact]
        public void MovieDataServiceMovieHasDataTest()
        {
            this.goodMockMovieDataService = new Mock<IMovieDataService>();
            this.goodMockMovieDataService.Setup(m => m.GetMovieAsync(0, "0076759")).
                Returns(Task.FromResult(this.movieJsonString));

            IMovieService movieService = new MovieService(
                null,
                this.goodMockMovieDataService.Object, 
                this.mockStoreRepository.Object);
            var movie = movieService.GetMovieAsync(0, "0076759").Result;
            
            Assert.Equal("0076759", movie.Id);
            mockStoreRepository.Verify(m => m.StoreValue("0:0076759", this.movieJsonString), Times.Once());
        }

        [Fact]
        public void MovieDataServiceMovieErrorTest()
        {
            this.badMockMovieDataService = new Mock<IMovieDataService>();
            this.badMockMovieDataService.Setup(m => m.GetMovieAsync(0, "0076759")).
                Returns(Task.FromResult(string.Empty));

            this.mockStoreRepository = new Mock<IStoreRepository>();
            this.mockStoreRepository.Setup(m => m.GetValue("0:0076759")).
                Returns(this.movieJsonString);

            IMovieService movieService = new MovieService(
                null,
                this.badMockMovieDataService.Object, 
                this.mockStoreRepository.Object);
            var movie = movieService.GetMovieAsync(0, "0076759").Result;

            Assert.Equal("0076759", movie.Id);
            mockStoreRepository.Verify(m => m.GetValue("0:0076759"), Times.Once());
        }

        private string moviesJsonString = @"{
            ""Movies"": [
                {
                    ""Title"": ""Star Wars: Episode IV - A New Hope"",
                    ""Year"": ""1977"",
                    ""Poster"": ""http://ia.media-imdb.com\/images\/M\/MV5BOTIyMDY2NGQtOGJjNi00OTk4LWFhMDgtYmE3M2NiYzM0YTVmXkEyXkFqcGdeQXVyNTU1NTcwOTk@._V1_SX300.jpg"",
                    ""ID"": ""cw0076759"",
                    ""Type"": ""movie""
                }
            ]
        }";

        private string movieJsonString = @"{
            ""Title"": ""Star Wars: Episode IV - A New Hope"",
            ""Year"": ""1977"",
            ""Rated"": ""PG"",
            ""Released"": ""25 May 1977"",
            ""Runtime"": ""121 min"",
            ""Genre"": ""Action, Adventure, Fantasy"",
            ""Director"": ""George Lucas"",
            ""Writer"": ""George Lucas"",
            ""Actors"": ""Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing"",
            ""Plot"": ""Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the galaxy from the Empire's world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader."",
            ""Language"": ""English"",
            ""Country"": ""USA"",
            ""Awards"": ""Won 6 Oscars. Another 48 wins & 28 nominations."",
            ""Poster"": ""http:\/\/ia.media-imdb.com\/images\/M\/MV5BOTIyMDY2NGQtOGJjNi00OTk4LWFhMDgtYmE3M2NiYzM0YTVmXkEyXkFqcGdeQXVyNTU1NTcwOTk@._V1_SX300.jpg"",
            ""Metascore"": ""92"",
            ""Rating"": ""8.7"",
            ""Votes"": ""915,459"",
            ""ID"": ""cw0076759"",
            ""Type"": ""movie"",
            ""Price"": ""123.5""
        }";
    }
}
