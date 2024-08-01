using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.Validations.FluentValidation;
using Core.Entities.Concrete;
using Core.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Dto;
using Dto.SearchDto;
using System.Linq.Expressions;

namespace Business.Concrete
{

    [LogAspect]
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;
        private readonly IOperationClaimDal _operationClaimDal;
        public UserManager(IUserDal userDal, IMapper mapper, IOperationClaimDal operationClaimDal)
        {
            _userDal = userDal;
            _mapper = mapper;
            _operationClaimDal = operationClaimDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IDataResult<User> Get(LoginDto dto)
        {
            var response = _userDal.Get(x => x.Email == dto.Email);
            if (response != null)
            {
                var mapped = _mapper.Map<User>(response);

                return new SuccessDataResult<User>(mapped);
            }

            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public IDataResult<List<OperationClaim>> GetClaims(UserDto user)
        {
            var response = _operationClaimDal.GetAll();
            return new SuccessDataResult<List<OperationClaim>>(response);
        }

        public  IDataResult<IEnumerable<UserResponseDto>> SearchAsync(UserSearchDto searchDto)
        {
            var includes = new List<string>()
            { 
                $"{nameof(User.UserOperationClaims)}",
                $"{nameof(User.UserOperationClaims)}.{nameof(UserOperationClaim.OperationClaim)}",
            };
            var predicate = GetExpression(searchDto);
            var result = _userDal.Search(predicate, true, includes).GetAwaiter().GetResult();
            return new SuccessDataResult<IEnumerable<UserResponseDto>>(_mapper.Map<IEnumerable<UserResponseDto>>(result));
        }

        private Expression<Func<User, bool>> GetExpression(UserSearchDto request)
        {
            var predicate = PredicateBuilder.True<User>();

            if (!String.IsNullOrEmpty(request.FirstName))
                predicate = predicate.And(p => p.FirstName == request.FirstName);
            if (!String.IsNullOrEmpty(request.Email))
                predicate = predicate.And(p => p.Email == request.Email);

            return predicate;
        }
    }
}
