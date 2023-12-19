using FinanceSystem.Abstractions.Models.Companies;

namespace FinanceSystem.Abstractions.Models.Payments;

public class PaymentDto : PaymentBaseDto
{
    public Guid Id { get; set; }
    public CompanyDto Company { get; set; }
    public bool IsTransfer { get; set; }
}