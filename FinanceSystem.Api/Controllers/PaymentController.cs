using FinanceSystem.Abstractions.Models.Payments;
using FinanceSystem.Filters;
using FinanceSystem.Services.Interfaces.Payments;
using Microsoft.AspNetCore.Mvc;

namespace FinanceSystem.Controllers;

[Route("api/payments")]
public sealed class PaymentController : BaseController
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    /// <summary>
    /// Add new payment
    /// </summary>
    /// <param name="paymentPostDto">Payment params</param>
    /// <returns>Guid of added payment</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ServiceFilter(typeof(ValidationActionFilter<PaymentPostDto>))]
    public Task<IActionResult> AddPayment([FromBody] PaymentPostDto paymentPostDto) => GetResult(async () =>
        await _paymentService.AddPayment(AuthorizedUserId, paymentPostDto));

    /// <summary>
    /// Edit the exist payment
    /// </summary>
    /// <param name="paymentId">Payment Id</param>
    /// <param name="paymentPostDto">Payment params</param>
    /// <returns></returns>
    [HttpPut("{paymentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ServiceFilter(typeof(ValidationActionFilter<PaymentPostDto>))]
    public Task<IActionResult> EditPayment([FromRoute] Guid paymentId, [FromBody] PaymentPostDto paymentPostDto) =>
        GetResult(async () => await _paymentService.EditPayment(AuthorizedUserId, paymentId, paymentPostDto));
}