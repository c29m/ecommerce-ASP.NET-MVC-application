using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicket.Data;

namespace MovieTicket.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly AppDbContext _appDbContext;


        public DirectorsController(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;

        }

        public async Task<IActionResult> Index()
        {
            var allSDirectors = await _appDbContext.Directors.ToListAsync();
            return View();
        }
    }
}
