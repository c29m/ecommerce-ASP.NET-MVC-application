using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MovieTicket.Data;

namespace MovieTicket.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _appDbContext;


        public MoviesController(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;

        }


        public async Task<IActionResult> Index()
        {
            var allMovies = await _appDbContext.Movies.Include(n => n.Cinema).ToListAsync();
            return View(allMovies);
        }
    }
}
