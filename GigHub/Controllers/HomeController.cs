﻿using GigHub.Models;
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



        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var upcomingGigs =
                _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs"
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