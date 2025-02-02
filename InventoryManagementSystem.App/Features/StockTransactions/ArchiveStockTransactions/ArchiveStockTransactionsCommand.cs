using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.App.Features.StockTransactions.ArchiveStockTransactions;

public record ArchiveStockTransactionsCommand() : IRequest<RequestResult<bool>>;
public class ArchiveStockTransactionsHandler : BaseRequestHandler<ArchiveStockTransactionsCommand, RequestResult<bool>>
{
    private readonly IRepository<StockTransaction> _stockTransRepo;

    public ArchiveStockTransactionsHandler(BaseRequestHandlerParam param, IRepository<StockTransaction> stockRepository) : base(param)
    {
        _stockTransRepo = stockRepository;
    }
    public override async Task<RequestResult<bool>> Handle(ArchiveStockTransactionsCommand request, CancellationToken cancellationToken)
    {


        var currentYear = DateTime.Now.Year;

        var res = await _stockTransRepo.GetAll()
                             .Where(a => a.Date.Year < currentYear)
                             .ExecuteUpdateAsync(setter => setter.SetProperty(t => t.IsArchieved, true));

        return res > 0 ? RequestResult<bool>.Success(true, "Success")
                       : RequestResult<bool>.Failure(ErrorCode.DataBaseError, "Failed to save to database");
    }

}
