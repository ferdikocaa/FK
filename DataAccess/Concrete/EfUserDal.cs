using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.UoW;

namespace DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, DatabaseContext>, IUserDal
    {
        private readonly IUnitOfWork _uow;
        public EfUserDal(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using var context = new DatabaseContext();
            var result = from operationClaim in context.OperationClaims
                         join userOperationClaim in context.UserOperationClaims
                             on operationClaim.ID equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.ID && userOperationClaim.IsDeleted == false
                         select new OperationClaim { ID = operationClaim.ID, Name = operationClaim.Name };
            return result.ToList();
        }
    }
}
