using MovieTicket.Data.Base;
using MovieTicket.Models;

namespace MovieTicket.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        public MoviesService(AppDbContext context): base(context)
        {

        }
    }
}
