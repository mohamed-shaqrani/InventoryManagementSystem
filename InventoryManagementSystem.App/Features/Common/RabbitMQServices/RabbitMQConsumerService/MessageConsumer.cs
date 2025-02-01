using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace InventoryManagementSystem.App.Features.Common.RabbitMQServices.RabbitMQConsumerService;

public class MessageConsumer : IHostedService
{
    IConnection _connection;
    IChannel _channel;

    public MessageConsumer()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;


    }
    public async Task ConsumeMessage<T>(T message)
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);
        await _channel.BasicPublishAsync(exchange: "newExchange", routingKey: "Test", body: body);
    }
    public async ValueTask DisposeAsync()
    {
        if (_channel != null)
            await _channel.CloseAsync();
        if (_connection != null)
            await _connection.CloseAsync();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += Consumer_RecieverAsync;
        await _channel.BasicConsumeAsync("newQueue", autoAck: false, consumer);
    }

    private Task Consumer_RecieverAsync(object sender, BasicDeliverEventArgs @event)
    {
        var body = Encoding.UTF8.GetString(@event.Body.ToArray());

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _channel.CloseAsync();
        await _connection.CloseAsync();

    }
}
