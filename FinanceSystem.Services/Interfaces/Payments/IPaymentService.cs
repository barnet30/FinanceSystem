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

    /// <summary>
    /// Get Payment by its id
    /// </summary>
    /// <param name="authorizedUserId">Authorized user id</param>
    /// <param name="paymentId">Payment Id</param>
    /// <returns></returns>
    Task<Result<PaymentDto>> GetPaymentById(Guid? authorizedUserId, Guid paymentId);
    
    /// <summary>
    /// Delete several payments
    /// </summary>
    /// <param name="authorizedUserId">Authorized user id</param>
    /// <param name="idsToDelete">payments ids to delete</param>
    /// <returns></returns>
    Task<Result> DeletePayments(Guid? authorizedUserId, List<Guid> idsToDelete);
}