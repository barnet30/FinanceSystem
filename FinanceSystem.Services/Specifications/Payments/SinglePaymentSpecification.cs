using Ardalis.Specification;
using FinanceSystem.Data.Entities;

namespace FinanceSystem.Services.Specifications.Payments;

public sealed class SinglePaymentSpecification : SingleResultSpecification<Payment>
{
    public SinglePaymentSpecification(Guid authorizedUserId, Guid paymentId)
    {
        Query
            .Where(x => !x.IsDeleted && x.Id == paymentId && x.User.Id == authorizedUserId)
            .Include(x => x.Company)
            .Include(x => x.PaymentCategory)
            .Include(x => x.Bank);
    }
}