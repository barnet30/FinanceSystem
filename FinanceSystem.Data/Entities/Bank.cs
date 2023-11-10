using System.ComponentModel.DataAnnotations;

namespace FinanceSystem.Data.Entities;

public class Bank : BaseEntity
{
    [Required]
    [MaxLength(150)]
    public string BankName { get; set; }

    public List<Payment> Payments { get; set; } = new();
}