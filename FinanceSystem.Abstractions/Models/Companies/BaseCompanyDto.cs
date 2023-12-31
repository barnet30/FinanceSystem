using FinanceSystem.Abstractions.Models.Location;

namespace FinanceSystem.Abstractions.Models.Companies;

public abstract class BaseCompanyDto
{
    public string FullName { get; set; }
    public string ShortName { get; set; }
    public string Inn { get; set; }
    public string Ogrn { get; set; }
    public LocationDto Location { get; set; }
}