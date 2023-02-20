using MovieTicket.Data.Base;
using MovieTicket.Data.ViewModels;
using MovieTicket.Models;

namespace MovieTicket.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownVM> GetNewMovieDropdownsValues();
    }
}
