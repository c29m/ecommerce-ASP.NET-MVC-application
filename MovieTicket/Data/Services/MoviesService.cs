using Microsoft.EntityFrameworkCore;
using MovieTicket.Data.Base;
using MovieTicket.Models;

namespace MovieTicket.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;

        public MoviesService(AppDbContext context): base(context)
        {
            _context = context;
        }


        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Director)
                .Include(am => am.MovieActors).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

    }
}
