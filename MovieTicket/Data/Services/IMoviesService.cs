using MovieTicket.Data.Base;
using MovieTicket.Models;

namespace MovieTicket.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
    }
}
