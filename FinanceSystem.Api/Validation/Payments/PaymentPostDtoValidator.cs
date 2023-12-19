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
            .Must(x => (x.CompanyId.HasValue && !x.IsTransfer) || (!x.CompanyId.HasValue && x.IsTransfer))
            .WithMessage("Следуеть указать место, где совершался платёж или был ли платёж переводом");
        
        When(x => x.CompanyId.HasValue, () =>
        {
            RuleFor(x => x.Location)
                .NotNull()
                .WithMessage("Для организаций должен быть указан адрес платежа");
        });
    }
}