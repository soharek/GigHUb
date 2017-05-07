using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IGigsRepository
    {
        IEnumerable<Gig> GetUsersUpcomingGigs(string userId);
        Gig GetGigWithAttendees(int id);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetSingleGig(int gigId);
        void AddGigToRepostory(Gig gig);
    }
}