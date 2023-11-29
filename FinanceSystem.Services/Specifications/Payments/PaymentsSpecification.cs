using Ardalis.Specification;
using FinanceSystem.Data.Entities;

namespace FinanceSystem.Services.Specifications.Payments;

public sealed class PaymentsSpecification : Specification<Payment>
{
    public PaymentsSpecification(Guid authorizedUserId, List<Guid> paymentIds)
    {
        Query.Where(x => !x.IsDeleted
                         && x.User.Id == authorizedUserId
                         && paymentIds.Contains(x.Id));
    }
}