using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IFollowingsRepository
    {
        Following GetFollowing(string userId, string artistId);
    }
}