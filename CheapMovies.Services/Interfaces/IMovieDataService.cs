using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheapMovies.Services
{
    public interface IMovieDataService
    {
        Task<string> GetMoviesAsync(int serviceId);
        Task<string> GetMovieAsync(int serviceId, string movieId);
    }
}