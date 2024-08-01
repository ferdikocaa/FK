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
    public class ActivityTypeManager : IActivityTypeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ActivityTypeManager(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [ValidationAspect(typeof(ActivityTypeValidator))]
        public IResult Add(ActivityTypeDto dto,UserContext userContext)
        {
            var mapped = _mapper.Map<ActivityType>(dto);
            mapped.IsDeleted = false;
            _unitOfWork.ActivityType.Add(mapped);
            return new SuccessResult(Messages.ProcessSuccessfull);
        }

        public IDataResult<List<ActivityTypeDto>> GetAll()
        {
            var response = _unitOfWork.ActivityType.GetAll();
            var mapped = _mapper.Map<List<ActivityTypeDto>>(response);
            return new SuccessDataResult<List<ActivityTypeDto>>(mapped);
        }

        public IDataResult<List<ActivityTypeDto>> GetAllWithFilter(Expression<Func<ActivityType, bool>> filter)
        {
            var response = _unitOfWork.ActivityType.GetAll(filter);
            var mapped = _mapper.Map<List<ActivityTypeDto>>(response);
            return new SuccessDataResult<List<ActivityTypeDto>>(mapped);
        }
    }
}
