﻿using AutoMapper;
using GigHub.App_Start;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GigHub
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            Mapper.Initialize(cfg=>cfg.AddProfile<MappingProfile>());
            //Mapper.Initialize(cfg =>
            //    {
            //        cfg.CreateMap<Genre, GenreDto>();
            //        cfg.CreateMap<ApplicationUser, UserDto>();
            //        cfg.CreateMap<Gig, GigDto>();
            //        cfg.CreateMap<Notification, NotificationDto>();

            //    }
            //);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
