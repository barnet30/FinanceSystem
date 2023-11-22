using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces.References;
using FinanceSystem.Data.Repositories.Base;

namespace FinanceSystem.Data.Repositories;

public sealed class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    public CompanyRepository(IFinanceSystemDbContext context) : base(context)
    {
    }
}