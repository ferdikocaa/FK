using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.UoW;


namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            #region Auth
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            #endregion End Of Auth

            #region User
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<UserManager>().As<IUserService>();
            #endregion End Of User

            #region SuccessLogs
            builder.RegisterType<EfSuccessLogDal>().As<ISuccessLogDal>();
            builder.RegisterType<SuccessLogManager>().As<ISuccessLogService>();
            #endregion End Of SuccessLogs

            #region ExceptionLogs
            builder.RegisterType<EfExceptionLogDal>().As<IExceptionLogDal>();
            builder.RegisterType<ExceptionLogManager>().As<IExceptionLogService>();
            #endregion End Of SuccessLogs

            #region UserOperationClaim
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            #endregion End Of UserOperationClaim

            #region OperationClaim
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            #endregion End Of OperationClaim

            #region Activity
            builder.RegisterType<EfActivityDal>().As<IActivityDal>();
            builder.RegisterType<ActivityManager>().As<IActivityService>();
            #endregion End Of UserOperationClaim


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
        }
    }
}
