using Core.Helpers;
using Core.Utilities.Results;
using Dto;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IActivityService
    {
        IResult Add(ActivityDto dto,UserContext userContext);
        IDataResult<List<ActivityDto>> GetAll();
        IDataResult<List<ActivityDto>> GetAllWithFilter(Expression<Func<ActivityDto, bool>> filter);
    }
}
