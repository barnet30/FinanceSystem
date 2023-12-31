using AutoMapper;
using FinanceSystem.Abstractions.Models.Companies;
using FinanceSystem.Abstractions.Models.Result;
using FinanceSystem.Common;
using FinanceSystem.Common.Constants;
using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces.Locations;
using FinanceSystem.Data.Interfaces.References;
using FinanceSystem.Services.Interfaces.Companies;
using FinanceSystem.Services.Specifications.Companies;
using Microsoft.EntityFrameworkCore;

namespace FinanceSystem.Services.Services;

public sealed class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public CompanyService(
        ICompanyRepository companyRepository,
        IMapper mapper,
        ILocationRepository locationRepository
    )
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
        _locationRepository = locationRepository;
    }

    public async Task<Result<Guid>> AddCompany(Guid? authorizedUserId, CompanyPostDto companyPostDto)
    {
        if (!authorizedUserId.HasValue)
            return Result<Guid>.Unauthorized(UserConstants.UnauthorizedUser);

        var mappedCompany = _mapper.Map<Company>(companyPostDto);

        await MapLocation(companyPostDto, mappedCompany);

        var result = await _companyRepository.InsertAsync(mappedCompany);

        return Result<Guid>.FromValue(result);
    }

    public async Task<Result> EditCompany(Guid? authorizedUserId, Guid companyId, CompanyPostDto companyPostDto)
    {
        if (!authorizedUserId.HasValue)
            return Result<Guid>.Unauthorized(UserConstants.UnauthorizedUser);

        var company = await _companyRepository.GetSingleAsync(new SingleCompanySpecification(companyId));
        if (company == null)
            return Result.NotFound(CompanyConstants.CompanyNotFound);

        _mapper.Map(companyPostDto, company);
        await MapLocation(companyPostDto, company);

        await _companyRepository.UpdateAsync(company);

        return Result.Success;
    }

    public async Task<Result> DeleteCompanies(Guid? authorizedUserId, List<Guid> idsToDelete)
    {
        if (!idsToDelete.Any())
            return Result.Success;
        
        if (!authorizedUserId.HasValue)
            return Result.Unauthorized(UserConstants.UnauthorizedUser);

        var companiesToDelete =
            (await _companyRepository.QueryAsync(x => idsToDelete.Contains(x.Id) && !x.IsDeleted))
            .Include(x => x.Location);

        if (!companiesToDelete.Any())
            return Result.Success;
        
        await _locationRepository.DeleteManyAsync(companiesToDelete.Select(x => x.Location.Id));
        await _companyRepository.DeleteManyAsync(companiesToDelete);

        return Result.Success;
    }

    public async Task<Result<CompanyDto>> GetCompanyById(Guid companyId)
    {
        var company =
            await _companyRepository.GetSingleAsync(new SingleCompanySpecification(companyId));

        return company == null
            ? Result<CompanyDto>.NotFound(CompanyConstants.CompanyNotFound)
            : Result<CompanyDto>.FromValue(_mapper.Map<CompanyDto>(company));
    }

    public async Task<Result<Page<CompanyDto>>> GetCompanyList(CompanySearchFilterDto filterDto)
    {
        if (filterDto.Page < 1)
            filterDto.Page = PagingParameters.DefaultPageNumber;
        
        var companies =
            await _companyRepository.QueryAsync(new SearchCompanySpecification(filterDto));

        if (companies == null)
            return Result<Page<CompanyDto>>.FromValue(Page<CompanyDto>.Empty);

        var total = companies.Count();
        
        var items = companies
            .Skip((filterDto.Page - 1) * filterDto.Count)
            .Take(filterDto.Count)
            .ToArray();

        var itemsMapped = _mapper.Map<CompanyDto[]>(items);

        return Result<Page<CompanyDto>>.FromValue(new Page<CompanyDto>
            { Items = itemsMapped, Total = total, TotalOnPage = filterDto.Count });
    }

    private async Task MapLocation(CompanyPostDto companyPostDto, Company company)
    {
        var newLocation = _mapper.Map<Location>(companyPostDto.Location);
        newLocation.Id = Guid.NewGuid();
        
        if (company.Location == null)
            company.Location = newLocation;
        
        else if (!company.Location.Equals(newLocation))
        {
            await _locationRepository.DeleteAsync(company.Location.Id);
            company.Location = newLocation;
        }
    }
}