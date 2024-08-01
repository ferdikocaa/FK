using Core.Entities.Concrete;
using Core.Utilities.Results;
using Dto;
using Dto.SearchDto;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(UserDto user);
        IDataResult<User> Get(LoginDto dto);
        IDataResult<IEnumerable<UserResponseDto>> SearchAsync(UserSearchDto user);
    }
}
