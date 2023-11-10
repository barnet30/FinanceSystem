using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace FinanceSystem.Data.Entities;

public class Location : BaseEntity
{ 
    [Required]
    [MaxLength(500)]
    public string Address { get; set; }
    
    [MaxLength(6)]
    public string PostCode { get; set; }
    
    public Point Coordinates { get; set; }

    public List<Payment> Payments { get; set; } = new();
}