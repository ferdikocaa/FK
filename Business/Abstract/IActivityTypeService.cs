using Core.Helpers;
using Core.Utilities.Results;
using Dto;
using Entities;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IActivityTypeService
    {
        IResult Add(ActivityTypeDto dto,UserContext userContext);
        IDataResult<List<ActivityTypeDto>> GetAll();
        IDataResult<List<ActivityTypeDto>> GetAllWithFilter(Expression<Func<ActivityType, bool>> filter);
    }
}
