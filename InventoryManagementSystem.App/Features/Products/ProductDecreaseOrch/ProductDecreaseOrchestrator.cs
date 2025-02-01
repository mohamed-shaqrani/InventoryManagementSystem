using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.DecreaseProductQuantity.Command;
using InventoryManagementSystem.App.Features.Products.HasEnoughStockForDecrease;
using InventoryManagementSystem.App.Features.Products.HasEnoughStockForDecrease.Query;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;

namespace InventoryManagementSystem.App.Features.Products.ProductDecreaseOrch;

public record ProductDecreaseOrchestrator(ProductDecreaseOrchRequestViewModel request) : IRequest<RequestResult<bool>>;
public class ProductDecreaseOrchestratorHandler : BaseRequestHandler<ProductDecreaseOrchestrator, RequestResult<bool>>
{
    public ProductDecreaseOrchestratorHandler(BaseRequestHandlerParam param) : base(param)
    {
    }

    public override async Task<RequestResult<bool>> Handle(ProductDecreaseOrchestrator request, CancellationToken cancellationToken)
    {
        var hasEnoughProductStock = await _mediator.Send(new HasEnoughStockForDecreaseQuery(request.request.Id, request.request.Quantity));

        return hasEnoughProductStock.IsSuccess ? await _mediator.Send(new DecreaseProductQuantityCommand(request.request.Id, request.request.Quantity))
                                               : hasEnoughProductStock;

    }
}
