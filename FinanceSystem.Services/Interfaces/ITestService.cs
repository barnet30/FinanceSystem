using FinanceSystem.Abstractions.Models.Result;

namespace FinanceSystem.Services.Interfaces;

public interface ITestService
{
    Task<Result> SendGrpcRequest(string query);
    Task<Result<bool>> SendIndexRequest(Guid paymentId);
}