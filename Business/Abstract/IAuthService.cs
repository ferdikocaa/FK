using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Dto;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<UserDto> Login(LoginDto loginDto);
        IDataResult<AccessToken> CreateAccessToken(UserDto user, int? currentUserId = null);
    }
}
