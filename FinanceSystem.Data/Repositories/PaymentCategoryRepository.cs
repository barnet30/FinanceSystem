using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces.References;
using FinanceSystem.Data.Repositories.Base;

namespace FinanceSystem.Data.Repositories;

public class PaymentCategoryRepository : BaseReferenceRepository<PaymentCategory>, IPaymentCategoryRepository
{
    public PaymentCategoryRepository(IFinanceSystemDbContext context) : base(context)
    {
    }
}