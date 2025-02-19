using InventoryManagementSystem.App.Features.Common.EmailService;
using InventoryManagementSystem.App.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json.Nodes;

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
        BasicMessage basicMessage = GetMessage(body);
        Type type;
        ConfigureMail(body);
        await _channel.BasicAckAsync(@event.DeliveryTag, multiple: false);
    }
    private async Task InvokeConsumer(BasicMessage basicMessage)
    {
        string type = basicMessage.Type;

        type = type.Replace("Messag","Consumer");
        string nameSpace = "InventoryManagementSystem.App.Features.Common.ConsumeMessages";
        Type getType = Type.GetType($"{nameSpace}.{type},InventoryManagementSystem");
        var conusmer =Activator.CreateInstance(getType);
        var method = getType.GetMethod("Consume");
          method.Invoke(conusmer, new object[] { basicMessage });



    }
    private BasicMessage GetMessage(string body)
    {
        var jsonObject = JObject.Parse(body);
        var typeName = jsonObject["Type"].ToString();
        var nameSpace = "InventoryManagementSystem.App.Features.Common.ConsumeMessages";
        Type type = Type.GetType($"{nameSpace}.{typeName},InventoryManagementSystem");
        var basicMessage = JsonConvert.DeserializeObject(body, type) as BasicMessage;
    
        return basicMessage ?? throw new NotImplementedException();
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
                    <p><strong>Date and Time:</strong> {dateAndTime.ToString("yyyy-MM-dd HH:mm:ss")} (UTC)</p>
                    <br/>
                    <p>Please take necessary actions to replenish the stock.</p>
                    <br/>
                    <p>Best Regards,</p>
                    <p>Your Inventory Management System</p>
                </body>
                </html>";
        _emailServices.SendEmail("mohamedshaqrani@gmail.com", "Warning: Low Product Quantity", emailBody, isBodyHtml: true);
    }
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _channel.CloseAsync();
        await _connection.CloseAsync();

    }
}
