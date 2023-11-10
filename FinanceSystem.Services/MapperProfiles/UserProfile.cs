using AutoMapper;
using FinanceSystem.Abstractions.Models.Users;
using FinanceSystem.Data.Entities;

namespace FinanceSystem.Services.MapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegisterDto, User>()
            .ForMember(dest => dest.Id, act => act.Ignore());
    }
        
}