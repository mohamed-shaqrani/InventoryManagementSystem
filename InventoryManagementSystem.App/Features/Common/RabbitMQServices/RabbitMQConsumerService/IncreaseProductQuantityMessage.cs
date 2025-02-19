using InventoryManagementSystem.App.Helpers;

namespace InventoryManagementSystem.App.Features.Common.RabbitMQServices.RabbitMQConsumerService;

public class IncreaseProductQuantityMessage :BasicMessage
{
    public int Quantity { get; set; }
    public int Id { get; set; }
    public override string Type =>  this.GetType().Name;

}
