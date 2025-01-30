using MediatR;

namespace InventoryManagementSystem.App.Features.Common;

public class BaseRequestHandlerParam
{
    readonly IMediator _mediator;

    public BaseRequestHandlerParam(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IMediator Mediator => _mediator;

}
