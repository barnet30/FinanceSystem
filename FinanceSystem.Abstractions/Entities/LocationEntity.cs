using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace FinanceSystem.Abstractions.Entities;

public class LocationEntity : BaseEntity
{ 
    [Required]
    [MaxLength(500)]
    public string Address { get; set; }
    
    [MaxLength(6)]
    public string PostCode { get; set; }
    
    public Point Coordinates { get; set; }

    public List<PaymentEntity> Payments { get; set; } = new();
}