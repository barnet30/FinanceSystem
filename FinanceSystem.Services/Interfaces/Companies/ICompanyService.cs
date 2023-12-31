using FinanceSystem.Abstractions.Models.Companies;
using FinanceSystem.Abstractions.Models.Result;
using FinanceSystem.Common;

namespace FinanceSystem.Services.Interfaces.Companies;

public interface ICompanyService
{
    /// <summary>
    /// Добавить организацию
    /// </summary>
    /// <param name="authorizedUserId">Идентификатор авторизованного пользователя</param>
    /// <param name="companyPostDto">Параметры организации</param>
    /// <returns></returns>
    Task<Result<Guid>> AddCompany(Guid? authorizedUserId, CompanyPostDto companyPostDto);

    /// <summary>
    /// Редактировать существующую организацию
    /// </summary>
    /// <param name="authorizedUserId">Идентификатор авторизованного пользователя</param>
    /// <param name="companyId">Идентификатор организации</param>
    /// <param name="companyPostDto">Параметры организации</param>
    /// <returns></returns>
    Task<Result> EditCompany(Guid? authorizedUserId, Guid companyId, CompanyPostDto companyPostDto);

    /// <summary>
    /// Удалить одну или несколько организаций
    /// </summary>
    /// <param name="authorizedUserId">Идентификатор авторизованного пользователя</param>
    /// <param name="idsToDelete">Идентификаторы организаций для удаления</param>
    /// <returns></returns>
    Task<Result> DeleteCompanies(Guid? authorizedUserId, List<Guid> idsToDelete);
    
    /// <summary>
    /// Получить организацию по идентификатору
    /// </summary>
    /// <param name="companyId">Идентификатор организации</param>
    /// <returns></returns>
    Task<Result<CompanyDto>> GetCompanyById(Guid companyId);
    
    /// <summary>
    /// Получить список оранизаций
    /// </summary>
    /// <param name="filterDto">Параметры фильтрации оранизаций</param>
    /// <returns></returns>
    Task<Result<Page<CompanyDto>>> GetCompanyList(CompanySearchFilterDto filterDto);
}