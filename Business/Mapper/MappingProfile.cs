using AutoMapper;
using Core.Entities.Concrete;
using Dto;
using Entities;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<UserResponseDto, User>().ReverseMap();
            CreateMap<UserOperationClaimDto, UserOperationClaim>().ReverseMap();
            CreateMap<OperationClaimDto, OperationClaim>().ReverseMap();

            CreateMap<OperationClaimDto, OperationClaim>().ReverseMap();


            CreateMap<UserResponseDto, User>().ReverseMap()
             .ForMember(m1 => m1.FullName, m2 => m2.MapFrom(x => x.FirstName))
             .ForMember(m1 => m1.Email, m2 => m2.MapFrom(x => x.Email))
             .ForMember(m1 => m1.UserOperationClaims, m2 => m2.MapFrom(x => x.UserOperationClaims));

            CreateMap<ActivityDto, Activity>().ReverseMap().ForMember(m1 => m1.ActivityName, m2 => m2.MapFrom(x => x.ActivityType.Name));
            CreateMap<ActivityTypeDto, ActivityType>().ReverseMap();

        }
    }
}
