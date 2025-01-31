namespace InventoryManagementSystem.App.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int LowStockThreshold { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }


}
