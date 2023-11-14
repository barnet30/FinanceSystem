using FinanceSystem.Abstractions.Models.References;
using FinanceSystem.Services.Interfaces.References;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceSystem.Controllers;

[Route("api/references")]
[AllowAnonymous]
public class ReferenceController : BaseController
{
    private readonly IReferenceService _referenceService;

    public ReferenceController(IReferenceService referenceService)
    {
        _referenceService = referenceService;
    }
    
    /// <summary>
    /// Get list of payment categories
    /// </summary>
    /// <returns></returns>
    [HttpGet("paymentCategories")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentCategoryDto>))]
    public Task<IActionResult> PaymentCategories() =>
        GetResult(async () => await _referenceService.PaymentCategories());
}