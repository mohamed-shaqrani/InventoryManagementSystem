using MediatR;

namespace InventoryManagementSystem.App.Features.StockTransactions.ArchiveStockTransactions;

public class StockTransactionBackgroundJobs
{
    private readonly IMediator _mediator;

    public StockTransactionBackgroundJobs(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task ArchiveStockTransactionsAsync()
    {
        await _mediator.Send(new ArchiveStockTransactionsCommand());
    }
}

