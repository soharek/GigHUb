using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.APIs
{
    [System.Web.Http.Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if(_context.Followings.Any(f=>f.FollowerId==userId && f.FolloweeId == dto.FolloweeId))
            {
                return BadRequest("Following already exists");
            }

            var following = new Following {FolloweeId = userId, FollowerId = dto.FolloweeId};

            _context.Followings.Add(following);
            _context.SaveChanges();


            return Ok();
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var followingToDelete =
                _context.Followings
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

            if (followingToDelete == null)
                return NotFound();

            _context.Followings.Remove(followingToDelete);
            _context.SaveChanges();

            return Ok();
        }


    }
}

