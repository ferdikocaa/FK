using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities;

namespace DataAccess.Concrete
{
    public class EfActivityTypeDal : EfEntityRepositoryBase<ActivityType, DatabaseContext>, IActivityTypeDal
    {
    }
}
