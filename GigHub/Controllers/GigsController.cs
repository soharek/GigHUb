using GigHub.Models;
using GigHub.Persistance;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.GigsRepository.GetUsersUpcomingGigs(userId);
               

            return View(gigs);
        }

        public ActionResult Attendig()
        {
            var userId = User.Identity.GetUserId();


            var viewModel = new GigsViewModel
            {
                UpcomingGigs =_unitOfWork.GigsRepository.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs i'm Attending",
                Attendance = _unitOfWork.AttendanceRepositories
                    .GetFutureAttendances(userId)
                    .ToLookup(a => a.GigId)

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
                Genres = _unitOfWork.GenreRepository.GetGenres(),
                Heading = "Create a Gig"
            };

            return View("GigForm",viewModel);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = _unitOfWork.GigsRepository.GetSingleGig(id);

            if (gig == null)
                return HttpNotFound();

            if(gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel
            {
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = _unitOfWork.GenreRepository.GetGenres(),
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
                viewModel.Genres = _unitOfWork.GenreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                Venue = viewModel.Venue,
                GenreId = viewModel.Genre,
                DateTime = viewModel.ToDateTime()

            };



            _unitOfWork.GigsRepository.AddGigToRepostory(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");

        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.GenreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gigInDb = _unitOfWork.GigsRepository.GetGigWithAttendees(viewModel.Id);

            if (gigInDb == null)
                return HttpNotFound();

            if(gigInDb.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();



            gigInDb.Modify(viewModel.ToDateTime(),viewModel.Venue,viewModel.Genre);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");

        }


        public ActionResult Details(int id)
        {

            var gig = _unitOfWork.GigsRepository.GetSingleGig(id);

            if (gig == null)
                return HttpNotFound();

            var viemodel = new GigDetailsViewModel { Gig = gig};

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viemodel.IsAttending = _unitOfWork.AttendanceRepositories.GetAttendance(gig.Id, userId) != null;

                viemodel.IsFollowing = _unitOfWork.FollowingsRepository.GetFollowing(userId, gig.ArtistId) != null;

            }


            return View("Details", viemodel);
        }

    }
}