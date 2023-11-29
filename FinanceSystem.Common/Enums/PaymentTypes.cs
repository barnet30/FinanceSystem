namespace FinanceSystem.Common.Enums;

[Flags]
public enum PaymentTypes : byte
{
    Income = 1,
    Expense = 2,
    Transfer = 4,
    Other = 8
}