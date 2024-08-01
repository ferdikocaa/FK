using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities;

namespace DataAccess.Concrete
{
    public class EfExceptionLogDal : EfEntityRepositoryBase<ExceptionLog, DatabaseContext>, IExceptionLogDal
    {
    }
}
