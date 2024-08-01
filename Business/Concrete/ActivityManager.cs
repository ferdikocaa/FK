using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.Validations.FluentValidation;
using Core.Helpers;
using Core.Utilities.Results;
using DataAccess.UoW;
using Dto;
using Entities;
using System.Linq.Expressions;

namespace Business.Concrete
{
    [LogAspect]
    public class ActivityManager : IActivityService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ActivityManager(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [ValidationAspect(typeof(ActivityValidator))]
        public IResult Add(ActivityDto dto,UserContext userContext)
        {
            var mapped = _mapper.Map<Activity>(dto);
            mapped.CreatedBy = userContext.SystemUserId;
            mapped.IsDeleted = false;
            _unitOfWork.Activity.Add(mapped);
            return new SuccessResult(Messages.ProcessSuccessfull);
        }

        public IDataResult<List<ActivityDto>> GetAll()
        {
            var includes = new List<string>()
            {
                $"{nameof(Activity.ActivityType)}",
            };
            var response = _unitOfWork.Activity.Search(null,true,includes : includes).GetAwaiter().GetResult();
            var mapped = _mapper.Map<List<ActivityDto>>(response);
            return new SuccessDataResult<List<ActivityDto>>(mapped);    
        }

        public IDataResult<List<ActivityDto>> GetAllWithFilter(Expression<Func<ActivityDto, bool>> filter)
        {
            var response = _unitOfWork.Activity.GetAll(filter);
            var mapped = _mapper.Map<List<ActivityDto>>(response);
            return new SuccessDataResult<List<ActivityDto>>(mapped);
        }
    }
}
