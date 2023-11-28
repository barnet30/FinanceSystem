using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceSystem.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace FinanceSystem.Data.Entities;

public class Payment : BaseEntity
{
    public double PaymentAmount { get; set; }
    
    public DateTime PaymentDate { get; set; }
    
    public PaymentTypes PaymentType { get; set; }
    
    [MaxLength(200)]
    public string Comment { get; set; }
    
    [Required]
    public Bank Bank { get; set; }
    
    public Company Company { get; set; }
    
    [Required]
    [InverseProperty("Payments")]
    public User User { get; set; }
    
    /// <summary>
    /// If it's transfer
    /// </summary>
    [InverseProperty("TransferPayment")]
    public User TransferUser { get; set; }
    
    public Location Location { get; set; }
    
    [Required]
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public PaymentCategory PaymentCategory { get; set; }
}