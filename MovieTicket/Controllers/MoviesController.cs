using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MovieTicket.Data;
using MovieTicket.Data.Services;

namespace MovieTicket.Controllers
{
    public class MoviesController : Controller
    {

        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {

            _moviesService = moviesService;

        }

        public async Task<IActionResult> Index()
        {
            var allMovies = await _moviesService.GetAllAsync(n => n.Cinema);
            return View(allMovies);
        }

        //Get: Movies/Details
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _moviesService.GetMovieByIdAsync(id);
            return View(movieDetails);  
        }

        //Get: Movie/Create

        public IActionResult Create()
        {
            ViewData["Welcome"] = "Welcome to our store";
            ViewBag.Description = "This is the store description";

            return View();
        }

        
    }
}
