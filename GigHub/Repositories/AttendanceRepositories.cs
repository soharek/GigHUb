using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class AttendanceRepositories : IAttendanceRepositories
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepositories(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public Attendance GetAttendance(int gigId, string userId)
        {
            return _context.Attendances.SingleOrDefault(x => x.GigId == gigId && x.AttendeeId == userId);
        }

    }
}