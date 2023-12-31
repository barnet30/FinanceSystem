using FinanceSystem.Abstractions.Models.Companies;
using FinanceSystem.Common;
using FinanceSystem.Services.Interfaces.Companies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceSystem.Controllers;

[Route("api/companies")]
public class CompanyController : BaseController
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    /// <summary>
    /// Добавить организацию
    /// </summary>
    /// <param name="companyPostDto">Параметры организации</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    public Task<IActionResult> AddCompany([FromBody] CompanyPostDto companyPostDto) => GetResult(async () =>
        await _companyService.AddCompany(AuthorizedUserId, companyPostDto));

    /// <summary>
    /// Редактировать существующую организацию
    /// </summary>
    /// <param name="companyId">Идентификатор организации</param>
    /// <param name="companyPostDto">Параметры организации</param>
    /// <returns></returns>
    [HttpPut("{companyId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public Task<IActionResult> EditCompany([FromRoute] Guid companyId, [FromBody] CompanyPostDto companyPostDto) =>
        GetResult(async () => await _companyService.EditCompany(AuthorizedUserId, companyId, companyPostDto));
    
    /// <summary>
    /// Удалить одну или несколько организаций
    /// </summary>
    /// <param name="idsToDelete">Идентификаторы организаций для удаления</param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public Task<IActionResult> DeleteCompanies([FromBody] List<Guid> idsToDelete) =>
        GetResult(async () => await _companyService.DeleteCompanies(AuthorizedUserId, idsToDelete));

    /// <summary>
    /// Получить организацию по идентификатору
    /// </summary>
    /// <param name="companyId">Идентификатор организации</param>
    /// <returns></returns>
    [HttpGet("{companyId:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public Task<IActionResult> GetCompanyById([FromRoute] Guid companyId) => GetResult(async () =>
        await _companyService.GetCompanyById(companyId));
    
    /// <summary>
    /// Получить список оранизаций
    /// </summary>
    /// <param name="filterDto">Параметры фильтрации оранизаций</param>
    /// <returns></returns>
    [HttpPost("list")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Page<CompanyDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    public Task<IActionResult> GetCompanyList([FromBody] CompanySearchFilterDto filterDto) =>
        GetResult(async () => await _companyService.GetCompanyList(filterDto));
}