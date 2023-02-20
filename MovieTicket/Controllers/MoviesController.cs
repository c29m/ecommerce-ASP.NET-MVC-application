using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MovieTicket.Data;
using MovieTicket.Models;
using MovieTicket.Data.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;

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

        public async Task<IActionResult> Create()
        {
           var movieDropdownData = await _moviesService.GetNewMovieDropdownsValues();
            ViewBag.Cinemas =  new SelectList(movieDropdownData.Cinemas, "Id", "Name" );

            ViewBag.Directors = new SelectList(movieDropdownData.Directors, "Id", "FullName");

            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownData = await _moviesService.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");

                ViewBag.Directors = new SelectList(movieDropdownData.Directors, "Id", "FullName");

                ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");

                return View(movie);

            }
            await _moviesService.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
     
    }
}
