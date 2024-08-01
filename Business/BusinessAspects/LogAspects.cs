using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.BusinessAspects
{
    public class LogAspect : MethodInterception
    {
        private readonly IExceptionLogDal _exceptionLogDal;
        private readonly ISuccessLogDal _successLogDal;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogAspect()
        {
            _exceptionLogDal = (EfExceptionLogDal)Activator.CreateInstance(typeof(EfExceptionLogDal));
            _successLogDal = (EfSuccessLogDal)Activator.CreateInstance(typeof(EfSuccessLogDal));
            _httpContextAccessor = (IHttpContextAccessor)ServiceTool.ServiceProvider.GetService(typeof(IHttpContextAccessor));
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _successLogDal.Add(new Entities.SuccessLog
            {
                MethodName = invocation.Method.Name,
                DeclaringType = invocation!.Method!.DeclaringType!.FullName,
                TargetType = invocation.TargetType.Name,
                TargetTypeFullName = invocation.TargetType.FullName,
                CreatedDate = DateTime.Now,
            });
        }
        protected override void OnException(IInvocation invocation, Exception e)
        {
            var clientUser = _httpContextAccessor?.HttpContext?.User?.Claims?.SingleOrDefault(x => x.Type == "Id");
            int userId = 0;

            int.TryParse(clientUser?.Value, out userId);
            _exceptionLogDal.Add(new Entities.ExceptionLog()
            {
                Class = invocation!.Method!.DeclaringType!.FullName,
                Method = invocation.Method.Name,
                Message = e?.Message,
                CreatedBy = userId,
                CreatedDate = DateTime.Now,
            });
        }
    }
}
