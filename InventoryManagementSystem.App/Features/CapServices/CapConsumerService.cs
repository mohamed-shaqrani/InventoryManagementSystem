using DotNetCore.CAP;
using InventoryManagementSystem.App.Helpers;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InventoryManagementSystem.App.Features.CapServices;

public class CapConsumerService : ICapSubscribe
{
    readonly IMediator _mediator;
    public CapConsumerService(IMediator mediator)
    {
        _mediator = mediator;
    }
    [CapSubscribe("decrease")]
    public async Task Consume(string message)
    {
        var basicMessage = GetMessage(message);
        InvokeConsumer(basicMessage);
    }
    private void InvokeConsumer(BasicMessage basicMessage)
    {
        var typetes = basicMessage.Type;
        var typeName = basicMessage.Type.Replace("Msg", "Consumer");
        var nameSpace = "InventoryManagementSystem.App.Features.CapServices.ConsumeCapMsg";
        var fullTypeName = $"{nameSpace}.{typeName}, InventoryManagementSystem.App";

        var consumerType = Type.GetType(fullTypeName);
        if (consumerType == null)
            throw new InvalidOperationException($"Consumer type '{fullTypeName}' not found.");

        var consumerInstance = Activator.CreateInstance(consumerType, _mediator);
        var consumeMethod = consumerType.GetMethod("Consume");

        consumeMethod?.Invoke(consumerInstance, new object[] { basicMessage });
    }

    private BasicMessage GetMessage(string body)
    {
        var jsonObject = JObject.Parse(body);
        var typeName = jsonObject["Type"]?.ToString();
        if (string.IsNullOrWhiteSpace(typeName))
            throw new ArgumentException("Message type not found in the body.");

        var nameSpace = "InventoryManagementSystem.App.Features.CapServices.ConsumeCapMsg";
        var type = Type.GetType($"{nameSpace}.{typeName}, InventoryManagementSystem.App");

        if (type == null)
            throw new InvalidOperationException($"Message type '{typeName}' not found.");

        var basicMessage = JsonConvert.DeserializeObject(body, type) as BasicMessage;
        return basicMessage ?? throw new InvalidOperationException("Deserialization failed.");
    }
}
