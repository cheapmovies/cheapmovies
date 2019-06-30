using System.Collections.Generic;
using System.Threading.Tasks;
using CheapMovies.Domain.Entities;

namespace CheapMovies.Services
{
    public interface IMovieService
    {
        List<string> GetProviders();
        Task<List<Movie>> GetMoviesAsync(int serviceId);
        Task<Movie> GetMovieAsync(int serviceId, string movieId);
    }
}