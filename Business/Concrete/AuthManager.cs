using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Dto;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;
        public AuthManager(IUserDal userDal,IMapper mapper,ITokenHelper tokenHelper)
        {
            _userDal = userDal;
            _mapper = mapper;   
            _tokenHelper = tokenHelper;
        }
        public IDataResult<AccessToken> CreateAccessToken(UserDto user, int? currentUserId = null)
        {
            var entity = _mapper.Map<User>(user);
            var claims = _userDal.GetClaims(entity);
            var accessToken = _tokenHelper.CreateToken(entity, claims,currentUserId);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        [LogAspect]
        public IDataResult<UserDto> Login(LoginDto loginDto)
        {
            var userCheck = _userDal.Get(x => x.Email == loginDto.Email);
            if (userCheck == null)
                return new ErrorDataResult<UserDto>(Messages.UserNotFound);
            var dto = _mapper.Map<UserDto>(userCheck);

            return new SuccessDataResult<UserDto>(dto, Messages.SuccessfulLogin);
        }
    }
}
