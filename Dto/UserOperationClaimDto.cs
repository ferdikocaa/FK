using Core.Entities.Concrete;

namespace Dto
{
    public class UserOperationClaimDto
    {
        public UserOperationClaimDto()
        {
            Claims = new List<OperationClaimDto>();
        }
        public List<OperationClaimDto> Claims { get; set; }
        public int UserId { get; set; }
    }
}
