﻿using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private AttendanceRepositories _attendanceRepositories;



        public HomeController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepositories = new AttendanceRepositories(_context);
        }

        

        public ActionResult Index(string query = null)
        {
            var userId = User.Identity.GetUserId();

            var upcomingGigs =
                _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs =
                    upcomingGigs
                    .Where(x => x.Genre.Name.Contains(query) ||
                    x.Artist.Name.Contains(query)||
                    x.Venue.Contains(query));
            }


            var attendances =
               _attendanceRepositories.GetFutureAttendances(userId)
                    .ToLookup(a=>a.GigId);


            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchWord = query,
                Attendance = attendances
            };


                


            return View("Gigs",viewModel);
        }

        public ActionResult About()
        {
           
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
           
                
            return View();
        }
    }
}