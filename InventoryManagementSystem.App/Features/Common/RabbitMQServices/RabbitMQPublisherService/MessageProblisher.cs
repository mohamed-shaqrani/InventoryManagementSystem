namespace InventoryManagementSystem.App.Features.Common.RabbitMQServices.RabbitMQPublisherService;
using System.Threading.Tasks;

public class MessagePublisher : IMessagePublisher//, IDisposable
{
    //private readonly IConnection _connection;
    //private readonly IModel _channel;

    //public MessagePublisher()
    //{
    //    var factory = new ConnectionFactory { HostName = "localhost" };
    //    _connection = factory.CreateConnection(); // Synchronous connection
    //    _channel = _connection.CreateModel();     // Synchronous channel creation

    //    // Declare exchange and queue synchronously
    //    _channel.ExchangeDeclare("newExchange", ExchangeType.Fanout, durable: true);
    //    _channel.QueueDeclare("newQueue", durable: true, exclusive: false, autoDelete: false);
    //    _channel.QueueBind("newQueue", "newExchange", "Test");
    //}

    //public Task PublishMessage<T>(T message)
    //{
    //    var jsonMessage = JsonSerializer.Serialize(message);
    //    var body = Encoding.UTF8.GetBytes(jsonMessage);

    //    // Use synchronous BasicPublish
    //    _channel.BasicPublish(exchange: "newExchange", routingKey: "Test",default, body: body);

    //    return Task.CompletedTask; // Keep method signature async-compatible
    //}

    //public void Dispose()
    //{
    //    _channel?.Close();
    //    _connection?.Close();
    //}
    public Task PublishMessage<T>(T message)
    {
        throw new NotImplementedException();
    }
}
