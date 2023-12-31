using System.Linq.Expressions;
using Ardalis.Specification;
using FinanceSystem.Abstractions.Models.Companies;
using FinanceSystem.Common;
using FinanceSystem.Data.Entities;

namespace FinanceSystem.Services.Specifications.Companies;

public sealed class SearchCompanySpecification : Specification<Company>
{
    public SearchCompanySpecification(CompanySearchFilterDto filter)
    {
        Expression<Func<Company, bool>> basicCriteria = x => !x.IsDeleted;
        
        Query
            .Where(basicCriteria)
            .Include(x => x.Location);
        
        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            if (filter.SortOrder == PagingSortOrder.Asc)
                Query.OrderBy(ResolveSortingColumn(filter.SortColumn)!);
            else
                Query.OrderByDescending(ResolveSortingColumn(filter.SortColumn)!);
        }
    }
    
    private static Expression<Func<Company, object>> ResolveSortingColumn(string sortColumn)
    {
        return sortColumn.ToLowerInvariant() switch
        {
            "shortname" => x => x.ShortName,
            _ => x => x.Id
        };
    }
}