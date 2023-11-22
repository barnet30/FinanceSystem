using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces.Payments;
using FinanceSystem.Data.Repositories.Base;

namespace FinanceSystem.Data.Repositories;

public sealed class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(IFinanceSystemDbContext context) : base(context)
    {
    }
}