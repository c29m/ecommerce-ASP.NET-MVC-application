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

        public async Task<NewMovieDropdownVM> GetNewMovieDropdownsValuesAsync()
        {
            var response = new NewMovieDropdownVM()
            {
                Actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync(),
                Directors = await _context.Directors.OrderBy(d => d.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync()

            };
            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
           var dbMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == data.Id);   

            if(dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description; 
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.Genre = data.Genre;
                dbMovie.DirectorId = data.DirectorId;
                await _context.SaveChangesAsync();
            }

            //Delete existing actors
            var existingActorsDb = _context.ActorsMovies.Where(n => n.MovieId == data.Id).ToList();
            _context.ActorsMovies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new ActorMovie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.ActorsMovies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
