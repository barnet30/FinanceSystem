using System.ComponentModel.DataAnnotations;
using FinanceSystem.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace FinanceSystem.Data.Entities;

/// <summary>
/// Платёж
/// </summary>
public class Payment : BaseEntity
{
    /// <summary>
    /// Сумма платежа
    /// </summary>
    public double PaymentAmount { get; set; }
    
    /// <summary>
    /// Дата платежа
    /// </summary>
    public DateTime PaymentDate { get; set; }
    
    /// <summary>
    /// Тип платежа
    /// </summary>
    public PaymentTypes PaymentType { get; set; }
    
    /// <summary>
    /// Комментарий
    /// </summary>
    [MaxLength(200)]
    public string Comment { get; set; }
    
    /// <summary>
    /// Банк, в котором совершён платёж
    /// </summary>
    [Required]
    public Bank Bank { get; set; }
    
    /// <summary>
    /// Организация, в которой был совершён платёж
    /// </summary>
    public Company Company { get; set; }
    
    /// <summary>
    /// Является ли платёж переводом
    /// </summary>
    public bool IsTransfer { get; set; }
    
    /// <summary>
    /// Автор платежа
    /// </summary>
    [Required]
    public User User { get; set; }
    
    /// <summary>
    /// Адрес платежа
    /// </summary>
    public Location Location { get; set; }
    
    /// <summary>
    /// Категория платежа
    /// </summary>
    [Required]
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public PaymentCategory PaymentCategory { get; set; }
}