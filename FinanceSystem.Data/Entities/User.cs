using System.ComponentModel.DataAnnotations;

namespace FinanceSystem.Data.Entities;

/// <summary>
/// User
/// </summary>
public class User : BaseEntity
{
    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }
    
    public DateTime? BirthDate { get; set; }

    [Required]
    public string Phone { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }

    public DateTime CreateDate { get; set; }
    
    public List<Payment> Payments { get; set; } = new();

    /// <summary>
    /// If user transfer someone
    /// </summary>
    public List<Payment> TransferPayment { get; set; } = new();
}