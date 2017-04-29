using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        
        public NottficationsAPIDataModel GetNewNotifications()
        {
            
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            var numberOfNewNotifications = notifications.Count;

            if (notifications.Count == 0)
            {
                notifications =
                    _context.UserNotifications
                        .Where(un => un.UserId == userId)
                        .Select(un => un.Notification)
                        .Include(n => n.Gig.Artist)
                        .OrderBy(t=>t.DateTime)
                        .Take(5)
                        .ToList();
                
            }

            var notificationDtos = notifications.Select(Mapper.Map<Notification, NotificationDto>);

            var notifcationsApiObject = new NottficationsAPIDataModel(notificationDtos,numberOfNewNotifications);

            return notifcationsApiObject;

        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications =
                _context.UserNotifications.Where(n => n.UserId == userId && !n.IsRead).ToList();

            notifications.ForEach(x=>x.Read());

            _context.SaveChanges();
            return Ok();
        }

    }
}
