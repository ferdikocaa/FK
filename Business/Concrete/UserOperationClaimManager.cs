using Business.Abstract;
using Business.BusinessAspects;

namespace Business.Concrete
{
    [LogAspect]
    public class UserOperationClaimManager : IUserOperationClaimService
    {
    }
}
