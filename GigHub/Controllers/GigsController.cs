using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

       
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            var artitstID = User.Identity.GetUserId(); //i take out users Id
            var artist = _context.Users.Single(u => u.Id == artitstID); // here i use the Id from line above
            var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);

            var gig = new Gig()
            {
                Artist = artist,
                Venue = viewModel.Venue,
                Genre = genre,
                DateTime = DateTime.Parse($"{viewModel.Date} {viewModel.Time}")

            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }
    }
}