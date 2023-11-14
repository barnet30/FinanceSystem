using AutoMapper;
using FinanceSystem.Abstractions.Models.References;
using FinanceSystem.Abstractions.Models.Result;
using FinanceSystem.Data.Interfaces.References;
using FinanceSystem.Services.Interfaces.References;

namespace FinanceSystem.Services.Services;

public class ReferenceService : IReferenceService
{
    private readonly IMapper _mapper;
    private readonly IPaymentCategoryRepository _paymentCategoryRepository;

    public ReferenceService(IPaymentCategoryRepository paymentCategoryRepository, IMapper mapper)
    {
        _paymentCategoryRepository = paymentCategoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<PaymentCategoryDto>>> PaymentCategories()
    {
        var result = await _paymentCategoryRepository.GetAll();
        return Result<List<PaymentCategoryDto>>.FromValue(_mapper.Map<List<PaymentCategoryDto>>(result));
    }
}