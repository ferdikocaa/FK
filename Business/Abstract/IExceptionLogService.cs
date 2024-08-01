using Core.Utilities.Results;
using Entities;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IExceptionLogService
    {
        IResult Add(ExceptionLog exceptionLog);
        IDataResult<List<ExceptionLog>> GetAll();
        IDataResult<List<ExceptionLog>> GetAllWithFilter(Expression<Func<ExceptionLog, bool>> filter);
    }
}
