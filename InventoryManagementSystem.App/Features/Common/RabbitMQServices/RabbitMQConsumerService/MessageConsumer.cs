using InventoryManagementSystem.App.Features.Common.EmailService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace InventoryManagementSystem.App.Features.Common.RabbitMQServices.RabbitMQConsumerService;

public class MessageConsumer : IHostedService
{
    IConnection _connection;
    IChannel _channel;
    IEmailServices _emailServices; 
    public MessageConsumer(IEmailServices emailServices)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;
        _emailServices = emailServices;


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

    private async Task Consumer_RecieverAsync(object sender, BasicDeliverEventArgs @event)
    {
        var body = Encoding.UTF8.GetString(@event.Body.ToArray());
        _emailServices.SendEmail("mohamedshaqrani@gmail.com", "Warning: Low Product Quanity", body);
       await _channel.BasicAckAsync(@event.DeliveryTag,multiple:false);
        //send mail
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _channel.CloseAsync();
        await _connection.CloseAsync();

    }
}
