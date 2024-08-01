using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities;

namespace DataAccess.Concrete
{
    public class EfActivityDal : EfEntityRepositoryBase<Activity, DatabaseContext>, IActivityDal
    {
    }
}
