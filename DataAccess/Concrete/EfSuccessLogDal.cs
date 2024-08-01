using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities;

namespace DataAccess.Concrete
{
    public class EfSuccessLogDal : EfEntityRepositoryBase<SuccessLog, DatabaseContext>, ISuccessLogDal
    {
    }
}
