namespace InventoryManagementSystem.App.Entities;
public class StockTransactionDetails : BaseEntity
{
    public int Quantity { get; set; }
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public TransactionType TransactionType { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<Product> Products { get; set; }

}