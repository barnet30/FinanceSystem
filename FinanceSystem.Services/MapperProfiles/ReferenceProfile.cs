using AutoMapper;
using FinanceSystem.Abstractions.Models.Companies;
using FinanceSystem.Abstractions.Models.References;
using FinanceSystem.Data.Entities;

namespace FinanceSystem.Services.MapperProfiles;

public class ReferenceProfile : Profile
{
    public ReferenceProfile()
    {
        CreateMap<PaymentCategory, PaymentCategoryDto>().ReverseMap();
        
        CreateMap<Bank, BankDto>().ReverseMap();
        
        CreateMap<Company, CompanyDto>().ReverseMap();
    }
}