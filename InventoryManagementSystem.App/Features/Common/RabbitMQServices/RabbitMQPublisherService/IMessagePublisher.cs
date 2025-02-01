namespace InventoryManagementSystem.App.Features.Common.RabbitMQServices.RabbitMQPublisherService;

public interface IMessagePublisher
{
    Task PublishMessage<T>(T message);

}
