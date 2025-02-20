using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.ProductQuntityMovement.DecreaseProductQuantity.Command;
using InventoryManagementSystem.App.Features.Products.ProductQuntityMovement.HasEnoughStockForDecrease.Query;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;

namespace InventoryManagementSystem.App.Features.Products.ProductQuntityMovement.ProductDecreaseOrch;

public record ProductDecreaseOrchestrator(int Id, int Quantity) : IRequest<RequestResult<bool>>;
public class ProductDecreaseOrchestratorHandler : BaseRequestHandler<ProductDecreaseOrchestrator, RequestResult<bool>>
{
    public ProductDecreaseOrchestratorHandler(BaseRequestHandlerParam param) : base(param)
    {
    }

    public override async Task<RequestResult<bool>> Handle(ProductDecreaseOrchestrator request, CancellationToken cancellationToken)
    {
        var hasEnoughProductStock = await _mediator.Send(new HasEnoughStockForDecreaseQuery(request.Id, request.Quantity));

        return hasEnoughProductStock.IsSuccess ? await _mediator.Send(new DecreaseProductQuantityCommand(request.Id, request.Quantity))
                                               : hasEnoughProductStock;

    }
}
