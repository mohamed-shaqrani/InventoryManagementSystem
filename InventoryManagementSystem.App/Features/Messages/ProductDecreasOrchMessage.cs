using InventoryManagementSystem.App.Helpers;

namespace InventoryManagementSystem.App.Features.Messages;

public class ProductDecreasOrchMessage : BasicMessage
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public override string Type => this.GetType().Name;
}
