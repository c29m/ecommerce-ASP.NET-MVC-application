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
            var allMovies = await _moviesService.GetAllAsync();
            return View(allMovies);
        }

        
    }
}
