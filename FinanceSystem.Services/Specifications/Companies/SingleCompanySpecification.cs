using Ardalis.Specification;
using FinanceSystem.Data.Entities;

namespace FinanceSystem.Services.Specifications.Companies;

public sealed class SingleCompanySpecification : SingleResultSpecification<Company>
{
    public SingleCompanySpecification(Guid companyId)
    {
        Query
            .Where(x => x.Id == companyId && !x.IsDeleted)
            .Include(x => x.Location);
    }
}