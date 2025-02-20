using DotNetCore.CAP;
using FluentValidation;
using InventoryManagementSystem.App.Features.Common.RabbitMQServices.RabbitMQPublisherService;
using MediatR;

namespace InventoryManagementSystem.App.Features.Common;

public class BaseEndpointParam<TRequest>
{
    readonly IMediator _mediator;
    readonly IMessagePublisher _imessagePublisher;
    readonly IValidator<TRequest> _validator;
    public IMediator Mediator => _mediator;
    public IValidator<TRequest> Validator => _validator;
    public IMessagePublisher MessagePublisher => _imessagePublisher;
    public ICapPublisher _capPublisher;
    public ICapPublisher CapPublisher => _capPublisher;

    public BaseEndpointParam(IMediator mediator, IMessagePublisher messagePublisher, IValidator<TRequest> validator, ICapPublisher capPublisher)
    {
        _mediator = mediator;
        _validator = validator;
        _imessagePublisher = messagePublisher;
        _capPublisher = capPublisher;
    }
}
