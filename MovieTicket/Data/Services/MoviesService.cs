using Microsoft.EntityFrameworkCore;
using MovieTicket.Data.Base;
using MovieTicket.Data.ViewModels;
using MovieTicket.Models;

namespace MovieTicket.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;

        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,   
                StartDate = data.StartDate, 
                EndDate = data.EndDate,
                Genre = data.Genre, 
                DirectorId = data.DirectorId,   
            };

            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            foreach(var actorId in data.ActorIds)
            {
                var newActorMovie = new ActorMovie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId,
                };
                await _context.ActorsMovies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
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

        public async Task<NewMovieDropdownVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownVM()
            {
                Actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync(),
                Directors = await _context.Directors.OrderBy(d => d.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync()

            };
            return response;
        }
    }
}
