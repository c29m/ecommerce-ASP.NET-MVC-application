using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicket.Data;


namespace MovieTicket.Controllers
{
    public class ActorsController : Controller
    {
        private readonly AppDbContext _appDbContext;


        public ActorsController(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;

        }

        public async Task<IActionResult> Index()
        {
            var allActors = await _appDbContext.Actors.ToListAsync();
            return View(allActors);
        }
    }
}
