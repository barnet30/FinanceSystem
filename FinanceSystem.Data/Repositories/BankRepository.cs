using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces.References;
using FinanceSystem.Data.Repositories.Base;

namespace FinanceSystem.Data.Repositories;

public class BankRepository : BaseReferenceRepository<Bank>, IBankRepository
{
    public BankRepository(IFinanceSystemDbContext context) : base(context)
    {
    }
}