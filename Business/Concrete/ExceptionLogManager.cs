using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class ExceptionLogManager : IExceptionLogService
    {
        private readonly IExceptionLogDal _exceptionLogDal;
        public ExceptionLogManager(IExceptionLogDal exceptionLogDal)
        {
            _exceptionLogDal = exceptionLogDal;
        }
        public IResult Add(ExceptionLog exceptionLog)
        {
            _exceptionLogDal.Add(exceptionLog);
            return new SuccessResult(Messages.ProcessSuccessfull);
        }

        public IDataResult<List<ExceptionLog>> GetAll()
        {
            return new SuccessDataResult<List<ExceptionLog>>(_exceptionLogDal.GetAll());
        }

        public IDataResult<List<ExceptionLog>> GetAllWithFilter(Expression<Func<ExceptionLog, bool>> filter)
        {
            return new SuccessDataResult<List<ExceptionLog>>(_exceptionLogDal.GetAll(filter));

        }
    }
}
