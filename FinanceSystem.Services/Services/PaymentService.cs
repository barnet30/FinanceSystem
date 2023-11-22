using AutoMapper;
using FinanceSystem.Abstractions.Models.Payments;
using FinanceSystem.Abstractions.Models.Result;
using FinanceSystem.Common.Constants;
using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces.Payments;
using FinanceSystem.Data.Interfaces.References;
using FinanceSystem.Data.Interfaces.Users;
using FinanceSystem.Services.Interfaces.Payments;

namespace FinanceSystem.Services.Services;

public sealed class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public PaymentService(IPaymentRepository paymentRepository, IUserRepository userRepository, IMapper mapper, ICompanyRepository companyRepository)
    {
        _paymentRepository = paymentRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _companyRepository = companyRepository;
    }

    public async Task<Result<Guid>> AddPayment(Guid? authorizedUserId, PaymentPostDto paymentPostDto)
    {
        if (!authorizedUserId.HasValue)
            return Result<Guid>.Unauthorized(UserConstants.UnauthorizedUser);

        var user = await _userRepository.GetByIdAsync(authorizedUserId.Value);
        if (user == null)
            return Result<Guid>.NotFound(UserConstants.UserNotFound);

        var mappedPayment = _mapper.Map<Payment>(paymentPostDto);
        mappedPayment.User = user;
        
        // map location
        if (!paymentPostDto.Location.Id.HasValue)
            mappedPayment.Location.Id = Guid.NewGuid();

        if (paymentPostDto.CompanyId.HasValue)
        {
            var company = await _companyRepository.GetByIdAsync(paymentPostDto.CompanyId.Value);
            if (company == null)
                return Result<Guid>.NotFound(CompanyConstants.CompanyNotFound);

            mappedPayment.Company = company;
        }
        
        if (paymentPostDto.TransferUserId.HasValue)
        {
            var transferUser = await _userRepository.GetByIdAsync(paymentPostDto.TransferUserId.Value);
            if (transferUser == null)
                return Result<Guid>.NotFound(UserConstants.UserNotFound);

            mappedPayment.TransferUser = transferUser;
        }
        
        var addedPaymentId = await _paymentRepository.InsertAsync(mappedPayment);
        return Result<Guid>.FromValue(addedPaymentId);
    }
}