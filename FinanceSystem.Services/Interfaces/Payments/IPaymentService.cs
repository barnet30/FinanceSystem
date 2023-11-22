using FinanceSystem.Abstractions.Models.Payments;
using FinanceSystem.Abstractions.Models.Result;

namespace FinanceSystem.Services.Interfaces.Payments;

public interface IPaymentService
{
    Task<Result<Guid>> AddPayment(Guid? authorizedUserId, PaymentPostDto paymentPostDto);
}