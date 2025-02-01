using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.App.Features.StockTransactions.AddStockTrans.Query;

public record IsQuantityValidQuery(List<StockTransDetailsViewModel> StockTransDetails) : IRequest<RequestResult<bool>>;
public class IsQuantityValidsHandler : BaseRequestHandler<IsQuantityValidQuery, RequestResult<bool>>
{
    private readonly IRepository<StockTransaction> _stockTransactionDetails;
    public IsQuantityValidsHandler(BaseRequestHandlerParam param, IRepository<StockTransaction> stockTransactionDetails) : base(param)
    {
        _stockTransactionDetails = stockTransactionDetails;
    }

    public override async Task<RequestResult<bool>> Handle(IsQuantityValidQuery request, CancellationToken cancellationToken)
    {
        var currentStock = await _stockTransactionDetails.GetAll()
                                                .Where(a => request.StockTransDetails.Select(y => y.ProductId).Contains(a.StockTransactionDetails.First().ProductId))
                                                .SelectMany(a => a.StockTransactionDetails)
                                                .GroupBy(d => d.ProductId)
                                                .Select(g => new
                                                {
                                                    ProductId = g.Key,
                                                    Added = g.Where(d => d.StockTransaction.TransactionType == StockTransactionType.Increase).Sum(d => d.Quantity),
                                                    Decreased = g.Where(d => d.StockTransaction.TransactionType == StockTransactionType.Decrease).Sum(d => d.Quantity)
                                                })
                                                .ToDictionaryAsync(
                                                    x => x.ProductId,
                                                    x => x.Added - x.Decreased,
                                                    cancellationToken
                                                );

        // Check if any requested quantity exceeds the available stock
        var isInvalid = request.StockTransDetails.Any(x =>
            currentStock.TryGetValue(x.ProductId, out var availableQty) && x.Quantity > availableQty);

        return isInvalid
            ? RequestResult<bool>.Failure(ErrorCode.InsufficientStock, "Not enough stock available.")
            : RequestResult<bool>.Success(true, "Stock quantity is valid.");
    }
}
