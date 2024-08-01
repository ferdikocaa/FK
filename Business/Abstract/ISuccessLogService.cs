using Core.Utilities.Results;
using Entities;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface ISuccessLogService
    {
        IDataResult<List<SuccessLog>> GetAll();
        IDataResult<List<SuccessLog>> GetAllWithFilter(Expression<Func<SuccessLog, bool>> filter);
    }
}
