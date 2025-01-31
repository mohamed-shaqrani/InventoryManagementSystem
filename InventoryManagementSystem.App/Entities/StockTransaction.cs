namespace InventoryManagementSystem.App.Entities;

public class StockTransaction : BaseEntity
{
    public DateTime Date { get; set; }
    public ICollection<StockTransactionDetails> StockTransactionDetails { get; set; }= [];

}
