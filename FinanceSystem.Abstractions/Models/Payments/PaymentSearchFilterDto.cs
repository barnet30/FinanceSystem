using FinanceSystem.Common;
using FinanceSystem.Common.Enums;

namespace FinanceSystem.Abstractions.Models.Payments;

public class PaymentSearchFilterDto : PagingSortParameters
{
    public PaymentTypes[] PaymentTypes { get; set; } = Array.Empty<PaymentTypes>();
    public List<int> PaymentCategoriesIds { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public List<int> BankIds { get; set; }
    
    /// <summary>
    /// Elastic search field
    /// </summary>
    public string Query { get; set; }
}