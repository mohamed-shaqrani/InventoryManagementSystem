using InventoryManagementSystem.App.Features.Common.EmailService;
using InventoryManagementSystem.App.Helpers;
using MediatR;
using Newtonsoft.Json;

namespace InventoryManagementSystem.App.Features.Common.RabbitMQServices.RabbitMQConsumerService;

using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class MessageConsumer : IHostedService, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IEmailServices _emailServices;
    private readonly IMediator _mediator;

    public MessageConsumer(IEmailServices emailServices, IMediator mediator)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection(); // Synchronous for pre-7 versions
        _channel = _connection.CreateModel();     // Synchronous model creation

        _emailServices = emailServices;
        _mediator = mediator;

        // Queue declaration ensures the queue exists
        _channel.QueueDeclare("newQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel); // Synchronous consumer for pre-7 versions
        consumer.Received += async (model, ea) =>
        {
            var body = Encoding.UTF8.GetString(ea.Body.ToArray());
            var basicMessage = GetMessage(body);
            InvokeConsumer(basicMessage);

            // Acknowledge the message
            _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            await Task.CompletedTask; // Ensure async lambda compatibility
        };

        _channel.BasicConsume(queue: "newQueue", autoAck: false, consumer: consumer);
        return Task.CompletedTask;
    }

    private void InvokeConsumer(BasicMessage basicMessage)
    {
        var typeName = basicMessage.Type.Replace("Message", "Consumer");
        var nameSpace = "InventoryManagementSystem.App.Features.Common.ConsumeMessages";
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

        var nameSpace = "InventoryManagementSystem.App.Features.Messages";
        var type = Type.GetType($"{nameSpace}.{typeName}, InventoryManagementSystem.App");

        if (type == null)
            throw new InvalidOperationException($"Message type '{typeName}' not found.");

        var basicMessage = JsonConvert.DeserializeObject(body, type) as BasicMessage;
        return basicMessage ?? throw new InvalidOperationException("Deserialization failed.");
    }

    private void ConfigureMail(string body)
    {
        var data = JsonConvert.DeserializeObject<dynamic>(body);

        string message = data.Message;
        DateTime dateAndTime = data.DateAndTime;

        string emailBody = $@"
            <html>
                <body>
                    <h2>Low Stock Alert</h2>
                    <p><strong>Message:</strong> {message}</p>
                    <p><strong>Date and Time:</strong> {dateAndTime:yyyy-MM-dd HH:mm:ss} (UTC)</p>
                    <br/>
                    <p>Please take necessary actions to replenish the stock.</p>
                    <br/>
                    <p>Best Regards,</p>
                    <p>Your Inventory Management System</p>
                </body>
            </html>";

        _emailServices.SendEmail("mohamedshaqrani@gmail.com", "Warning: Low Product Quantity", emailBody, isBodyHtml: true);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _channel?.Close();
        _connection?.Close();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
