namespace InventoryManagementSystem.App.Entities;

public class StockTransaction : BaseEntity
{
    public DateTime Date { get; set; }
    public StockTransactionType TransactionType { get; set; }
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<StockTransactionDetails> StockTransactionDetails { get; set; } = [];

}
