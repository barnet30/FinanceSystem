using System.ComponentModel.DataAnnotations;

namespace FinanceSystem.Data.Entities;

public class Bank : BaseReferenceEntity
{
    public List<Payment> Payments { get; set; } = new();
}