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
    /// <param name="paymentPostDto"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ServiceFilter(typeof(ValidationActionFilter<PaymentPostDto>))]
    public Task<IActionResult> AddPayment([FromBody] PaymentPostDto paymentPostDto) => GetResult(async () =>
        await _paymentService.AddPayment(AuthorizedUserId, paymentPostDto));
}