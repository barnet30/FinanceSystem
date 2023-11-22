namespace FinanceSystem.Abstractions.Models.Location;

public sealed class LocationDto
{
    public Guid? Id { get; set; }
    public string Address { get; set; }
    public string PostCode { get; set; }
    public CoordinateDto Coordinates { get; set; }
}