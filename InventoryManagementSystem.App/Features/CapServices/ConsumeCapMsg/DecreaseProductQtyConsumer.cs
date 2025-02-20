using InventoryManagementSystem.App.Features.Products.ProductQuntityMovement.ProductDecreaseOrch;
using MediatR;

namespace InventoryManagementSystem.App.Features.CapServices.ConsumeCapMsg;

public class DecreaseProductQtyConsumer
{
    readonly IMediator _mediator;

    public DecreaseProductQtyConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task Consume(DecreaseProductQtyMsg decreaseProductQty)
    {
        var decreaseProduct = await _mediator.Send(new ProductDecreaseOrchestrator(decreaseProductQty.ProductId, decreaseProductQty.Quantity));
        return;
    }
}
