using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace InventoryManagementSystem.App.Features.Common.RabbitMQServices.RabbitMQPublisherService;

public class MessagePublisher : IMessagePublisher
{
    IConnection _connection;
    IChannel _channel;

    public MessagePublisher()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;
        _channel.ExchangeDeclareAsync("newExchange", ExchangeType.Fanout, durable: true);
        _channel.QueueBindAsync("newQueue", "newExchange", "Test");

    }
    public async Task PublishMessage<T>(T message)
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);
        await _channel.BasicPublishAsync(exchange: "newExchange", routingKey: "Test", body: body);
    }
}
