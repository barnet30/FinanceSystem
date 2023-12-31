using FinanceSystem.Common;

namespace FinanceSystem.Abstractions.Models.Companies;

public class CompanySearchFilterDto : PagingSortParameters
{
    public string Query { get; set; }
}