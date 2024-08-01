using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimExtensions
	{
		public static void AddEmail(this ICollection<Claim> claims, string email)
		{
			claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
		}

		public static void AddName(this ICollection<Claim> claims, string name)
		{
			claims.Add(new Claim("nameSurname", name));
		}

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            var roleStr = string.Join(",", roles);
            claims.Add(new Claim("userRoles", roleStr));
        }

        public static void AddCurrentUserId(this ICollection<Claim> claims, int? currentUserId)
        {
            claims.Add(new Claim("currentUserId", currentUserId.HasValue ? currentUserId.ToString() : ""));
        }

    }
}
