using FinanceSystem.Abstractions.Models.Payments;
using FluentValidation;

namespace FinanceSystem.Validation.Payments;

public sealed class PaymentPostDtoValidator : AbstractValidator<PaymentPostDto>
{
    public PaymentPostDtoValidator()
    {
        RuleFor(x => x.PaymentAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Сумма платежа не может быть отрицательной");

        RuleFor(x => x.PaymentType)
            .IsInEnum()
            .WithMessage("Неверное значение для типа платежа");

        When(x => !string.IsNullOrWhiteSpace(x.Comment), () =>
        {
            RuleFor(x => x.Comment)
                .MaximumLength(200)
                .WithMessage("Комментарий к платежу не может превышать 200 символов");
        });

        RuleFor(x => x)
            .Must(x => (x.CompanyId.HasValue && !x.TransferUserId.HasValue) ||
                       (!x.CompanyId.HasValue && x.TransferUserId.HasValue))
            .WithMessage(
                "Следуеть указать место, где совершался платёж или пользователя, которому был совершён платёж");


    }
}