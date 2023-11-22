namespace FinanceSystem.Data.Entities;

public sealed class Company : BaseEntity
{
    public string FullName { get; set; }
    public string ShortName { get; set; }
    public string Inn { get; set; }
    public string Ogrn { get; set; }

    public List<Payment> Payments { get; set; } = new();
}