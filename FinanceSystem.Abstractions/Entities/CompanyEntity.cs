namespace FinanceSystem.Abstractions.Entities;

public class CompanyEntity : BaseEntity
{
    public string FullName { get; set; }
    public string ShortName { get; set; }
    public string Inn { get; set; }
    public string Ogrn { get; set; }

    public List<PaymentEntity> Payments { get; set; } = new();
}