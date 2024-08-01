using Core.Entities.Concrete;

namespace Dto
{
    public class UserResponseDto
    {
        public int ID { get; set; }
        public bool IsActive { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public virtual IEnumerable<UserOperationClaimDto>? UserOperationClaims { get; set; }
    }
}
