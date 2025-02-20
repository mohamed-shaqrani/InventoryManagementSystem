using Autofac;
using DotNetCore.CAP;
using InventoryManagementSystem.App.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InventoryManagementSystem.App.Features.CapServices;

public class CapConsumerService : ICapSubscribe
{
    private readonly ILifetimeScope _lifetimeScope;

    public CapConsumerService(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }
    [CapSubscribe("decrease")]
    public async Task Consume(string message)
    {
        using (var scope = _lifetimeScope.BeginLifetimeScope())
        {
            var basicMessage = GetMessage(message);
            await InvokeConsumerAsync(scope, basicMessage);
        }

    }
    private async Task InvokeConsumerAsync(ILifetimeScope scope, BasicMessage basicMessage)
    {
        var typetes = basicMessage.Type;
        var typeName = basicMessage.Type.Replace("Msg", "Consumer");
        var nameSpace = "InventoryManagementSystem.App.Features.CapServices.ConsumeCapMsg";
        var fullTypeName = $"{nameSpace}.{typeName}, InventoryManagementSystem.App";

        var consumerType = Type.GetType(fullTypeName);
        if (consumerType == null)
            throw new InvalidOperationException($"Consumer type '{fullTypeName}' not found.");

        // Resolve the consumer instance within the new scope
        var consumerInstance = scope.Resolve(consumerType);
        var consumeMethod = consumerType.GetMethod("Consume");

        if (consumeMethod != null)
        {
            await (Task)consumeMethod.Invoke(consumerInstance, new object[] { basicMessage });
        }
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
