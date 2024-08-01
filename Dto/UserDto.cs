using Core.Entities.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto
{
    public class UserDto
    {
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }
        public bool? IsActive { get; set; }
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        public virtual IEnumerable<UserOperationClaim>? UserOperationClaims { get; set; }
    }
}
