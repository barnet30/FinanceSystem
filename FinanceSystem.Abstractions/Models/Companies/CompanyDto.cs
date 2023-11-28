namespace FinanceSystem.Abstractions.Models.Companies;

public class CompanyDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string ShortName { get; set; }
    public string Inn { get; set; }
    public string Ogrn { get; set; }
}