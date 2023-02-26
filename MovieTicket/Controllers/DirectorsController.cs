using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MovieTicket.Data;
using MovieTicket.Data.Services;
using MovieTicket.Models;

namespace MovieTicket.Controllers
{
    [Authorize]
    public class DirectorsController : Controller
    {
        private readonly IDirectorsService _directorsService;

        public DirectorsController(IDirectorsService directorsService)
        {

            _directorsService = directorsService;

        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allDirectors = await _directorsService.GetAllAsync();
            return View(allDirectors);
        }

        //Get: Actors/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio ")] Director director)
        {

            if (!ModelState.IsValid)
            {
                return View(director);
            }

            await _directorsService.AddAsync(director);

            return RedirectToAction(nameof(Index));

        }

        //Get: Actors/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var directorDetails = await _directorsService.GetByIdAsync(id);

            if (directorDetails == null) return View("NotFound");
            return View(directorDetails);
        }

        //GET: producers/edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var directorDetails = await _directorsService.GetByIdAsync(id);
            if (directorDetails == null) return View("NotFound");
            return View(directorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Director director)
        {
            if (!ModelState.IsValid) return View(director);

            if (id ==director.Id)
            {
                await _directorsService.UpdateAsync(id, director);
                return RedirectToAction(nameof(Index));
            }
            return View(director);
        }

        //Get: Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var directorDetails = await _directorsService.GetByIdAsync(id);
            if (directorDetails == null) return View("NotFound");
            return View(directorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var directorDetails = await _directorsService.GetByIdAsync(id);
            if (directorDetails == null) return View("NotFound");

            await _directorsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
