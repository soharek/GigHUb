using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.APIs
{
    [System.Web.Http.Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context =new ApplicationDbContext();
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            string userId = User.Identity.GetUserId();
            //var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId); old version
            var gig = _context.Gigs.Include(x=>x.Attendances.Select(u=>u.Attendee)).Single(g => g.Id == id && g.ArtistId == userId); //nowa wersja wykorzystujaca linq

            if (gig.IsCanceled)
                return NotFound();

            gig.IsCanceled = true;

            var notification = new Notification(gig, NotificationType.GigCanceled);
            
            // stara wersja która wykonywała dwa zapytania do bazy
            //var attendees = _context.Attendances old impementetion
            //    .Where(g => g.GigId == gig.Id)
            //    .Select(a => a.Attendee)
            //    .ToList();

            foreach (var attendee in gig.Attendances.Select(a=>a.Attendee))  // nowa wersja wykorzystuąca lambdy i linq
            {
                attendee.Notify(notification);
               
            }
           

            _context.SaveChanges();

            return Ok();
        }
    }
}
