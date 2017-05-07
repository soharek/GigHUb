using GigHub.Repositories;

namespace GigHub.Persistance
{
    public interface IUnitOfWork
    {
        IGigsRepository GigsRepository { get; }
        IAttendanceRepositories AttendanceRepositories { get; }
        IFollowingsRepository FollowingsRepository { get; }
        IGenreRepository GenreRepository { get; }
        void Complete();
    }
}