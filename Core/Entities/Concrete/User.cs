namespace Core.Entities.Concrete
{
    public class User :EntityMain, IEntity
	{
		public int ID { get; set; }
		public string? FirstName { get; set; }
		public string? Email { get; set; }
		public byte[] PasswordSalt { get; set; }
		public byte[] PasswordHash { get; set; }
		public bool IsActive { get; set; }
		public virtual IEnumerable<UserOperationClaim> UserOperationClaims { get; set; }
	}
}
