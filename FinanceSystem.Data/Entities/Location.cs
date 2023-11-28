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
    
    public override bool Equals(object obj)
    {
        if (obj is Location location)
            return Address == location.Address && EqualsCoordinates(Coordinates, location.Coordinates);
        return false;
    }
    
    private static bool EqualsCoordinates(Point p1, Point p2)
    {
        const double tolerance = 0.00001;
        return Math.Abs(p1.X - p2.X) < tolerance && Math.Abs(p1.Y - p2.Y) < tolerance;
    }
}