using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheapMovies.Domain.Entities;
using CheapMovies.Services;

namespace CheapMovies.Api.Controllers
{
    [ApiController]
    public class MovieController : Controller
    {
        private IMovieService movieService { get; set; }

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }
        
        [HttpGet]
        [Route("api/movie/getproviders")]
        public ActionResult GetProviders()
        {
            try
            {
                var result = this.movieService.GetProviders();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return new EmptyResult();
            }
        }

        [HttpGet]
        [Route("api/movie/getmovies")]
        public async Task<ActionResult> GetMoviesAsync(int serviceId)
        {
            try
            {
                var result = await this.movieService.GetMoviesAsync(serviceId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return new EmptyResult();
            }
        }

        [HttpGet]
        [Route("api/movie/getmovie")]
        public async Task<ActionResult> GetMovieAsync(int serviceId, string movieId)
        {
            try
            {
                var result = await this.movieService.GetMovieAsync(serviceId, movieId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return new EmptyResult();
            }
        }
    }
}
