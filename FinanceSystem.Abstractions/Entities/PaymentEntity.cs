using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceSystem.Common.Enums;

namespace FinanceSystem.Abstractions.Entities;

public class PaymentEntity : BaseEntity
{
    public double PaymentAmount { get; set; }
    
    public DateTime PaymentDate { get; set; }
    
    public PaymentCategories PaymentCategory { get; set; } = PaymentCategories.Others;
    
    public PaymentTypes PaymentType { get; set; }
    
    [MaxLength(200)]
    public string Comment { get; set; }
    
    [Required]
    public BankEntity Bank { get; set; }
    
    public CompanyEntity Company { get; set; }
    
    [Required]
    [InverseProperty("Payments")]
    public UserEntity User { get; set; }
    
    /// <summary>
    /// If it's transfer
    /// </summary>
    [InverseProperty("TransferPayment")]
    public UserEntity TransferUser { get; set; }

    [Required]
    public LocationEntity Location { get; set; }
}