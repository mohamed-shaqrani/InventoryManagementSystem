using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;

namespace InventoryManagementSystem.App.Features.StockTransactions.AddStockTrans.Command;

public record AddStockTransCommand(DateTime Date, StockTransactionType TransactionType, int UserId, int WarehouseId, List<StockTransDetailsViewModel> StockTransDetails) : IRequest<RequestResult<bool>>;
public class AddStockTransHandler : BaseRequestHandler<AddStockTransCommand, RequestResult<bool>>
{
    private readonly IRepository<StockTransaction> _productRepository;

    public AddStockTransHandler(BaseRequestHandlerParam param, IRepository<StockTransaction> stockRepository) : base(param)
    {
        _productRepository = stockRepository;
    }
    public override async Task<RequestResult<bool>> Handle(AddStockTransCommand request, CancellationToken cancellationToken)
    {


        var stockTrans = new StockTransaction
        {
            Date = request.Date,
            TransactionType = request.TransactionType,
            UserId = request.UserId,
            WarehouseId = request.WarehouseId,
            StockTransactionDetails = request.StockTransDetails.Select(x => new StockTransactionDetails
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList()
        };

        await _productRepository.AddAsync(stockTrans);
        var res = await _productRepository.SaveChangesAsync();

        return res > 0 ? RequestResult<bool>.Success(true, "Success")
                       : RequestResult<bool>.Failure(ErrorCode.DataBaseError, "Failed to save to database");
    }

}
