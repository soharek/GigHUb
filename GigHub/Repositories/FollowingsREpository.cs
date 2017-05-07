using GigHub.Models;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowingsRepository : IFollowingsRepository
    {
        private ApplicationDbContext _context;

        public FollowingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string userId, string artistId)
        {
            return _context.Followings.SingleOrDefault(x => x.FolloweeId == artistId && x.FollowerId == userId);
        }


    }
}