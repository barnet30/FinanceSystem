using AutoMapper;
using FinanceSystem.Abstractions.Models.Payments;
using FinanceSystem.Data.Entities;
using FinanceSystem.Services.Resolver;

namespace FinanceSystem.Services.MapperProfiles;

public sealed class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<PaymentPostDto, Payment>()
            .ForMember(dest => dest.PaymentCategory,
                act =>
                {
                    act.PreCondition(dest => dest.PaymentCategory != null);
                    act.MapFrom<ReferenceItemMapResolver<PaymentCategory>, int>(src => src.PaymentCategory.Id);
                })
            .ForMember(dest => dest.Bank, act =>
            {
                act.PreCondition(dest => dest.Bank != null);
                act.MapFrom<ReferenceItemMapResolver<Bank>, int>(src => src.Bank.Id);
            })
            .ForMember(dest => dest.Location, act => act.Ignore());

        CreateMap<Payment, PaymentDto>()
            .ForMember(dest => dest.TransferUserId,
                act => act.MapFrom(src => src.TransferUser != null ? src.TransferUser.Id : (Guid?)null));
    }
}