using AutoMapper;
using FinanceSystem.Abstractions.Models.Companies;
using FinanceSystem.Data.Entities;

namespace FinanceSystem.Services.MapperProfiles;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<CompanyPostDto, Company>()
            .ForMember(dest => dest.Location, act => act.Ignore());

        CreateMap<Company, CompanyDto>();
    }
}