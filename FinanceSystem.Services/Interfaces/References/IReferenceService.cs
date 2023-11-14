using FinanceSystem.Abstractions.Models.References;
using FinanceSystem.Abstractions.Models.Result;

namespace FinanceSystem.Services.Interfaces.References;

public interface IReferenceService
{
    Task<Result<List<PaymentCategoryDto>>> PaymentCategories();
}