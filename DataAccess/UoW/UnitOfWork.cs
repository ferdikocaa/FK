using DataAccess.Abstract;
using DataAccess.Concrete;

namespace DataAccess.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;

        private DatabaseContext _databaseContext;
        public IActivityDal Activity { get;private set; }
        public IActivityTypeDal ActivityType { get; private set; }
 
        public UnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            Activity = new EfActivityDal();
            ActivityType = new EfActivityTypeDal();
        }

        public async Task SaveChangesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }


        public void SaveChanges()
        {
            _databaseContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _databaseContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
