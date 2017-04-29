using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
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
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = 
                _context.Gigs.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g=>g.Genre)
                .ToList();

            return View(gigs);
        }

        public ActionResult Attendig()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Attendances
                .Where(a => a.AttendeeId ==userId)
                .Select(g=>g.Gig)
                .Include(g=>g.Artist)
                .Include(g=>g.Genre)
                .ToList();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs i'm Attending"
                
            };



            return View("Gigs", viewModel);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel model)
        {
            
            return RedirectToAction("Index","Home", new {query = model.SearchWord});
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Create a Gig"
            };

            return View("GigForm",viewModel);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId==userId );

            var viewModel = new GigFormViewModel
            {
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
                
            };

            return View("GigForm",viewModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            var a = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                Venue = viewModel.Venue,
                GenreId = viewModel.Genre,
                DateTime = viewModel.ToDateTime()

            };



            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");

        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            string userId = User.Identity.GetUserId();
            var gigInDb = _context.Gigs
                .Include(x=>x.Attendances.Select(u=>u.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId ==userId);



            gigInDb.Modify(viewModel.ToDateTime(),viewModel.Venue,viewModel.Genre);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");

        }


        public ActionResult Details(int id)
        {

            var gig = _context.Gigs
                .Where(g => g.Id == id)
                .Include(x => x.Artist)
                .Include(g=>g.Genre)
                .SingleOrDefault();

            if (gig == null)
                return HttpNotFound();

            var viemodel = new GigDetailsViewModel { Gig = gig};

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viemodel.IsAttending = _context.Attendances.Any(g => g.GigId == gig.Id && g.AttendeeId == userId);

                viemodel.IsFollowing =
                    _context.Followings.Any(g => g.FollowerId == userId && g.FolloweeId == gig.ArtistId);
            }


            return View("Details", viemodel);
        }

    }

    public class GigDetailsViewModel
    {
        public Gig Gig { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsAttending { get; set; }
    }
}