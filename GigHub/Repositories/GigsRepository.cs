using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class GigsRepository : IGigsRepository
    {
        private readonly ApplicationDbContext _context;

        public GigsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetUsersUpcomingGigs(string userId)
        {
            return _context.Gigs.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }


        public Gig GetGigWithAttendees(int id)
        {
            return _context.Gigs
                .Include(x => x.Attendances.Select(u => u.Attendee))
                .SingleOrDefault(g => g.Id == id);
        }

        
        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(g => g.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetSingleGig(int gigId)
        {
            return _context.Gigs
                .Include(a=>a.Artist)
                .Include(g=>g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public void AddGigToRepostory(Gig gig)
        {
            _context.Gigs.Add(gig);
            
        }

    }
}