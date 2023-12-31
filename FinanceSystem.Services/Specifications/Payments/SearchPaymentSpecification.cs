using System.Linq.Expressions;
using Ardalis.Specification;
using FinanceSystem.Abstractions.Models.Payments;
using FinanceSystem.Common;
using FinanceSystem.Data.Entities;

namespace FinanceSystem.Services.Specifications.Payments;

public sealed class SearchPaymentSpecification : Specification<Payment>
{
    public SearchPaymentSpecification(Guid userId, PaymentSearchFilterDto filter)
    {
        Expression<Func<Payment, bool>> basicCriteria = x => !x.IsDeleted && x.User.Id == userId;
        Expression<Func<Payment, bool>> typesCriteria = x => true;
        Expression<Func<Payment, bool>> categoriesCriteria = x => true;
        Expression<Func<Payment, bool>> datesCriteria = x => true;
        Expression<Func<Payment, bool>> banksCriteria = x => true;

        if (filter.PaymentTypes.Length > 0)
            typesCriteria = x => filter.PaymentTypes.Contains(x.PaymentType);

        if (filter.PaymentCategoriesIds != null && filter.PaymentCategoriesIds.Any())
            categoriesCriteria = x => filter.PaymentCategoriesIds.Contains(x.PaymentCategory.Id);

        if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            datesCriteria = x => x.PaymentDate >= filter.DateFrom.Value && x.PaymentDate <= filter.DateTo.Value;
        else if (filter.DateFrom.HasValue && !filter.DateTo.HasValue)
            datesCriteria = x => x.PaymentDate >= filter.DateFrom.Value;
        else if (!filter.DateFrom.HasValue && filter.DateTo.HasValue)
            datesCriteria = x => x.PaymentDate <= filter.DateTo.Value;

        if (filter.BankIds != null && filter.BankIds.Any())
            banksCriteria = x => filter.BankIds.Contains(x.Bank.Id);

        Query
            .Where(basicCriteria)
            .Where(typesCriteria)
            .Where(categoriesCriteria)
            .Where(datesCriteria)
            .Where(banksCriteria)
            .Include(x => x.Bank)
            .Include(x => x.Company)
            .Include(x => x.PaymentCategory);

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            if (filter.SortOrder == PagingSortOrder.Asc)
                Query.OrderBy(ResolveSortingColumn(filter.SortColumn)!);
            else
                Query.OrderByDescending(ResolveSortingColumn(filter.SortColumn)!);
        }
    }
    
    private static Expression<Func<Payment, object>> ResolveSortingColumn(string sortColumn)
    {
        return sortColumn.ToLowerInvariant() switch
        {
            "paymentamount" => x => x.PaymentAmount,
            "date" => x => x.PaymentDate,
            "type" => x => x.PaymentType,
            "category" => x => x.PaymentCategory.Name,
            _ => x => x.PaymentDate
        };
    }
}