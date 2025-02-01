namespace InventoryManagementSystem.App.Entities;
public class StockTransactionDetails : BaseEntity
{
    public int Quantity { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
    public StockTransaction StockTransaction { get; set; }

}