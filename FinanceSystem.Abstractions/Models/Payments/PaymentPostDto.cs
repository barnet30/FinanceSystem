using FinanceSystem.Abstractions.Models.Location;
using FinanceSystem.Abstractions.Models.References;
using FinanceSystem.Common.Enums;

namespace FinanceSystem.Abstractions.Models.Payments;

public sealed class PaymentPostDto
{
    public double PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentCategoryDto PaymentCategory { get; set; }
    public PaymentTypes PaymentType { get; set; }
    public string Comment { get; set; }
    public BankDto Bank { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? TransferUserId { get; set; }
    public LocationDto Location { get; set; }
}