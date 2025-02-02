using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;

namespace InventoryManagementSystem.App.Features.StockTransactions.DecreaseStockTrans.Command;

public record DecreaseStockTransCommand(DateTime Date, StockTransactionType TransactionType, int UserId, int WarehouseId, int ProductId, int Quantity) : IRequest<RequestResult<bool>>;
public class DecreaseStockTransHandler : BaseRequestHandler<DecreaseStockTransCommand, RequestResult<bool>>
{
    private readonly IRepository<StockTransaction> _stockTransRepo;

    public DecreaseStockTransHandler(BaseRequestHandlerParam param, IRepository<StockTransaction> stockRepository) : base(param)
    {
        _stockTransRepo = stockRepository;
    }
    public override async Task<RequestResult<bool>> Handle(DecreaseStockTransCommand request, CancellationToken cancellationToken)
    {


        var stockTrans = new StockTransaction
        {
            Date = request.Date,
            TransactionType = StockTransactionType.Decrease,
            UserId = request.UserId,
            WarehouseId = request.WarehouseId,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };

        await _stockTransRepo.AddAsync(stockTrans);
        var res = await _stockTransRepo.SaveChangesAsync();

        return res > 0 ? RequestResult<bool>.Success(true, "Success")
                       : RequestResult<bool>.Failure(ErrorCode.DataBaseError, "Failed to save to database");
    }

}
