using FinanceSystem.Abstractions.Models.Result;
using FinanceSystem.Data.Interfaces.Payments;
using FinanceSystem.Search;
using FinanceSystem.Services.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace FinanceSystem.Services.Services;

public class TestService : ITestService
{
    private readonly Greeter.GreeterClient _client;
    private readonly FinanceSystemSearch.FinanceSystemSearchClient _searchClient;
    private readonly IPaymentRepository _paymentRepository;

    public TestService(Greeter.GreeterClient client, FinanceSystemSearch.FinanceSystemSearchClient searchClient, IPaymentRepository paymentRepository)
    {
        _client = client;
        _searchClient = searchClient;
        _paymentRepository = paymentRepository;
    }

    public async Task<Result> SendGrpcRequest(string query)
    {
        await _client.SayHelloAsync(new HelloRequest
        {
            Name = query
        });

        return Result.Success;
    }


    public async Task<Result<bool>> SendIndexRequest(Guid paymentId)
    {
        var payment = (await _paymentRepository.QueryAsync(x => x.Id == paymentId))
            .Include(x => x.Company).ThenInclude(x => x.Location)
            .FirstOrDefault();

        if (payment == null)
            return Result<bool>.NotFound("not found");

        var res = await _searchClient.ReindexPaymentAsync(new PaymentIndex
        {
            Id = payment.Id.ToString(),
            CompanyAddress = payment.Company.Location.Address,
            CompanyShortName = payment.Company.ShortName,
            CompanyFullName = payment.Company.FullName,
            PaymentDate = Timestamp.FromDateTime(payment.PaymentDate)
        });

        return Result<bool>.FromValue(res.IsSuccess);
    }
}