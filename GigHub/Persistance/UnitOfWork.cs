using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigsRepository GigsRepository { get; private set; }
        public IAttendanceRepositories AttendanceRepositories { get; private set; }
        public IFollowingsRepository FollowingsRepository { get; private set; }
        public IGenreRepository GenreRepository { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            GigsRepository = new GigsRepository(context);
            AttendanceRepositories = new AttendanceRepositories(context);
            FollowingsRepository = new FollowingsRepository(context);
            GenreRepository = new GenreRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

    }
}