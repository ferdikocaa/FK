using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete
{
    public class UserOperationClaim : EntityMain, IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
        [JsonIgnore]
        [NotMapped]
        public override User CreatedUser { get => base.CreatedUser; set => base.CreatedUser = value; }
        [JsonIgnore]
        [NotMapped]
        public override User UpdatedUser { get => base.UpdatedUser; set => base.UpdatedUser = value; }
    }
}
