using AutoMapper;
using FinanceSystem.Abstractions.Models.Payments;
using FinanceSystem.Abstractions.Models.Result;
using FinanceSystem.Common.Constants;
using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces.Locations;
using FinanceSystem.Data.Interfaces.Payments;
using FinanceSystem.Data.Interfaces.References;
using FinanceSystem.Data.Interfaces.Users;
using FinanceSystem.Services.Interfaces.Payments;
using FinanceSystem.Services.Specifications.Payments;

namespace FinanceSystem.Services.Services;

public sealed class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public PaymentService(IPaymentRepository paymentRepository,
        IUserRepository userRepository,
        IMapper mapper,
        ICompanyRepository companyRepository,
        ILocationRepository locationRepository
    )
    {
        _paymentRepository = paymentRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _companyRepository = companyRepository;
        _locationRepository = locationRepository;
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

        await MapLocation(paymentPostDto, mappedPayment);

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

    public async Task<Result> EditPayment(Guid? authorizedUserId, Guid paymentId, PaymentPostDto paymentPostDto)
    {
        if (!authorizedUserId.HasValue)
            return Result.Unauthorized(UserConstants.UnauthorizedUser);
        
        var user = await _userRepository.GetByIdAsync(authorizedUserId.Value);
        if (user == null)
            return Result.NotFound(UserConstants.UserNotFound);

        var payment =
            await _paymentRepository.GetSingleAsync(new SinglePaymentSpecification(authorizedUserId.Value, paymentId));
        if (payment == null)
            return Result.NotFound(PaymentConstants.PaymentNotFound);

        _mapper.Map(paymentPostDto, payment);
        await MapLocation(paymentPostDto, payment);

        await _paymentRepository.UpdateAsync(payment);
        
        return Result.Success;
    }

    public async Task<Result<PaymentDto>> GetPaymentById(Guid? authorizedUserId, Guid paymentId)
    {
        if (!authorizedUserId.HasValue)
            return Result<PaymentDto>.Unauthorized(UserConstants.UnauthorizedUser);

        var payment =
            await _paymentRepository.GetSingleAsync(new SinglePaymentSpecification(authorizedUserId.Value, paymentId));

        return payment == null
            ? Result<PaymentDto>.NotFound(PaymentConstants.PaymentNotFound)
            : Result<PaymentDto>.FromValue(_mapper.Map<PaymentDto>(payment));
    }

    private async Task MapLocation(PaymentPostDto paymentPostDto, Payment payment)
    {
        var newLocation = _mapper.Map<Location>(paymentPostDto.Location);
        newLocation.Id = Guid.NewGuid();
        
        if (payment.Location == null)
            payment.Location = newLocation;
        
        else if (!payment.Location.Equals(newLocation))
        {
            await _locationRepository.DeleteAsync(payment.Location.Id);
            payment.Location = newLocation;
        }
    }
}