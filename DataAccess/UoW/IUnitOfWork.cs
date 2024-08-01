using DataAccess.Abstract;

namespace DataAccess.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IActivityDal Activity { get; }
        IActivityTypeDal ActivityType { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
