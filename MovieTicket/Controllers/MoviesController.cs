using Microsoft.AspNetCore.Mvc;
using MovieTicket.Models;
using MovieTicket.Data.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using MovieTicket.Data.Static;

namespace MovieTicket.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {

        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {

            _moviesService = moviesService;

        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allMovies = await _moviesService.GetAllAsync(n => n.Cinema);
            return View(allMovies);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _moviesService.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteraResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower())
                                                    ||
                                                    n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                return View(nameof(Index), filteraResult); 
            }
            return View(nameof(Index),allMovies);
        }

        //Get: Movies/Details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _moviesService.GetMovieByIdAsync(id);
            return View(movieDetails);  
        }

        //Get: Movies/Create

        public async Task<IActionResult> Create()
        {
           var movieDropdownsData = await _moviesService.GetNewMovieDropdownsValuesAsync();
            ViewBag.Cinemas =  new SelectList(movieDropdownsData.Cinemas, "Id", "Name" );

            ViewBag.Directors = new SelectList(movieDropdownsData.Directors, "Id", "FullName");

            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _moviesService.GetNewMovieDropdownsValuesAsync();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");

                ViewBag.Directors = new SelectList(movieDropdownsData.Directors, "Id", "FullName");

                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }
            await _moviesService.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

       
        //GET: Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _moviesService.GetMovieByIdAsync(id);
            if (movieDetails == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                Genre = movieDetails.Genre,
                CinemaId = movieDetails.CinemaId,
                DirectorId = movieDetails.DirectorId,
                ActorIds = movieDetails.MovieActors.Select(n => n.ActorId).ToList(),
            };

            var movieDropdownsData = await _moviesService.GetNewMovieDropdownsValuesAsync();
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Directors, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _moviesService.GetNewMovieDropdownsValuesAsync();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Directors, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await _moviesService.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}
