namespace InventoryManagementSystem.App.Features.Common.RabbitMQServices.RabbitMQConsumerService;

public interface IMessageConsumer : IHostedService
{
    Task ConsumeMessage<T>(T message);

}
