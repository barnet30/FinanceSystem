using FinanceSystem.Abstractions.Models.Location;
using FinanceSystem.Abstractions.Models.References;
using FinanceSystem.Common.Enums;

namespace FinanceSystem.Abstractions.Models.Payments;

public class PaymentBaseDto
{
    public double PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentCategoryDto PaymentCategory { get; set; }
    public string Comment { get; set; }
    public BankDto Bank { get; set; }
    public LocationDto Location { get; set; }
    public PaymentTypes PaymentType { get; set; }
}