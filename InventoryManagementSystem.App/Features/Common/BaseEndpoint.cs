using DotNetCore.CAP;
using FluentValidation;
using InventoryManagementSystem.App.Features.Common.RabbitMQServices.RabbitMQPublisherService;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.Endpint;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.Common;

[ApiController]
public class BaseEndpoint<TRequest, TResponse> : ControllerBase
{
    protected IValidator<TRequest> _validator;
    protected IMediator _mediator;
    protected IMessagePublisher _rabbitMQPubService;
    protected ICapPublisher _capPublisher;

    public BaseEndpoint(BaseEndpointParam<TRequest> param)
    {
        _validator = param.Validator;
        _mediator = param.Mediator;
        _rabbitMQPubService = param.MessagePublisher;
        _capPublisher = param.CapPublisher;

    }
    protected EndpointResponse<TResponse> ValidateRequest(TRequest request)
    {
        var validateResult = _validator.Validate(request);
        if (!validateResult.IsValid)
        {
            var errors = string.Join(",", validateResult.Errors.Select(x => x.ErrorMessage));
            return EndpointResponse<TResponse>.Failure(ErrorCode.ValidationError, errors);
        }
        return EndpointResponse<TResponse>.Success(default, "");
    }
}
