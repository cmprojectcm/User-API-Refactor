using AutoMapper;
using Tests.User.Api.Dto;

namespace Tests.User.Api.Extensions
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles () {
            CreateMap<Models.User, UserDto>().PreserveReferences();
            CreateMap<UserDto, Models.User>().PreserveReferences();
        }
    }
}