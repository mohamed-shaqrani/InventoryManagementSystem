using Hangfire;
using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.StockTransactions.ArchiveStockTransactions;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;

namespace InventoryManagementSystem.App.Features.StockTransactions.AddStockTrans.Command;

public record AddStockTransCommand(DateTime Date, StockTransactionType TransactionType, int UserId, int WarehouseId, int ProductId, int Quantity) : IRequest<RequestResult<bool>>;
public class AddStockTransHandler : BaseRequestHandler<AddStockTransCommand, RequestResult<bool>>
{
    private readonly IRepository<StockTransaction> _stocTransRepo;

    public AddStockTransHandler(BaseRequestHandlerParam param, IRepository<StockTransaction> stockRepository) : base(param)
    {
        _stocTransRepo = stockRepository;
    }

    public override async Task<RequestResult<bool>> Handle(AddStockTransCommand request, CancellationToken cancellationToken)
    {
        var stockTrans = new StockTransaction
        {
            Date = request.Date,
            TransactionType = request.TransactionType,
            UserId = request.UserId,
            WarehouseId = request.WarehouseId,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };

        await _stocTransRepo.AddAsync(stockTrans);
        var res = await _stocTransRepo.SaveChangesAsync();
        if (res > 0)
        {
            var count = _stocTransRepo.GetAll().Count();
            //depends on the Business we can modify this 
            if (count > 1)
            {
                BackgroundJob.Enqueue<StockTransactionBackgroundJobs>(x => x.ArchiveStockTransactionsAsync());
            }
        }
        return res > 0 ? RequestResult<bool>.Success(true, "Success")
                       : RequestResult<bool>.Failure(ErrorCode.DataBaseError, "Failed to save to database");
    }

}
