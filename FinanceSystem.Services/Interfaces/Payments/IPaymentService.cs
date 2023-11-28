using FinanceSystem.Abstractions.Models.Payments;
using FinanceSystem.Abstractions.Models.Result;

namespace FinanceSystem.Services.Interfaces.Payments;

public interface IPaymentService
{
    /// <summary>
    /// Add new payment
    /// </summary>
    /// <param name="authorizedUserId">Authorized user id</param>
    /// <param name="paymentPostDto">Payment params</param>
    /// <returns></returns>
    Task<Result<Guid>> AddPayment(Guid? authorizedUserId, PaymentPostDto paymentPostDto);

    /// <summary>
    /// Edit the exist payment
    /// </summary>
    /// <param name="authorizedUserId">Authorized user id</param>
    /// <param name="paymentId">Payment Id</param>
    /// <param name="paymentPostDto">Payment params</param>
    Task<Result> EditPayment(Guid? authorizedUserId, Guid paymentId, PaymentPostDto paymentPostDto);
}