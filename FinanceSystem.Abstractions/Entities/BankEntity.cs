using System.ComponentModel.DataAnnotations;

namespace FinanceSystem.Abstractions.Entities;

public class BankEntity : BaseEntity
{
    [Required]
    [MaxLength(150)]
    public string BankName { get; set; }

    public List<PaymentEntity> Payments { get; set; } = new();
}