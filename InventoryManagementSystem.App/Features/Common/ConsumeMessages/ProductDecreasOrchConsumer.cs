using DotNetCore.CAP;
using InventoryManagementSystem.App.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InventoryManagementSystem.App.Features.Common.ConsumeMessages;

public class ProductDecreasOrchConsumer : ICapSubscribe
{
    //  readonly IMediator _mediator;
    public ProductDecreasOrchConsumer()
    {
        //_mediator = mediator;
    }
    [CapSubscribe("captest")]
    public async Task Consume(string message)
    {
        // var decreaseProduct = await _mediator.Send(new ProductDecreaseOrchestrator(decreasOrchMessage.ProductId, decreasOrchMessage.Quantity));
        //   var basicMessage = GetMessage(message);
        //    InvokeConsumer(basicMessage);
    }
    private void InvokeConsumer(BasicMessage basicMessage)
    {
        var typeName = basicMessage.Type.Replace("Message", "Consumer");
        var nameSpace = "InventoryManagementSystem.App.Features.Common.ConsumeMessages";
        var fullTypeName = $"{nameSpace}.{typeName}, InventoryManagementSystem.App";

        var consumerType = Type.GetType(fullTypeName);
        if (consumerType == null)
            throw new InvalidOperationException($"Consumer type '{fullTypeName}' not found.");

        var consumerInstance = Activator.CreateInstance(consumerType);
        var consumeMethod = consumerType.GetMethod("Consume");

        consumeMethod?.Invoke(consumerInstance, new object[] { basicMessage });
    }

    private BasicMessage GetMessage(string body)
    {
        var jsonObject = JObject.Parse(body);
        var typeName = jsonObject["Type"]?.ToString();
        if (string.IsNullOrWhiteSpace(typeName))
            throw new ArgumentException("Message type not found in the body.");

        var nameSpace = "InventoryManagementSystem.App.Features.Messages";
        var type = Type.GetType($"{nameSpace}.{typeName}, InventoryManagementSystem.App");

        if (type == null)
            throw new InvalidOperationException($"Message type '{typeName}' not found.");

        var basicMessage = JsonConvert.DeserializeObject(body, type) as BasicMessage;
        return basicMessage ?? throw new InvalidOperationException("Deserialization failed.");
    }
}
