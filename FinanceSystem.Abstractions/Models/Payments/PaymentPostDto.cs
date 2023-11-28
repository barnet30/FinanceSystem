namespace FinanceSystem.Abstractions.Models.Payments;

public sealed class PaymentPostDto : PaymentBaseDto
{
    public Guid? CompanyId { get; set; }
    public Guid? TransferUserId { get; set; }
}