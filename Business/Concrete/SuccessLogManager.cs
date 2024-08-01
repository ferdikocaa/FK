using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class SuccessLogManager : ISuccessLogService
    {
        private readonly ISuccessLogDal _successLogDal;
        public SuccessLogManager(ISuccessLogDal successLogDal)
        {
            _successLogDal = successLogDal;
        }
        public IDataResult<List<SuccessLog>> GetAll()
        {
            var response = _successLogDal.GetAll();
            return new SuccessDataResult<List<SuccessLog>>(response);   
        }

        public IDataResult<List<SuccessLog>> GetAllWithFilter(Expression<Func<SuccessLog, bool>> filter)
        {
            var response = _successLogDal.GetAll(filter);
            return new SuccessDataResult<List<SuccessLog>>(response);
        }
    }
}
