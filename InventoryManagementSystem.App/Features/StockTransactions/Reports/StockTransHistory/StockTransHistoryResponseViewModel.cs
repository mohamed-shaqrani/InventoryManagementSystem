using InventoryManagementSystem.App.Entities;

namespace InventoryManagementSystem.App.Features.StockTransactions.Reports.StockTransHistory;

public class GetStockTransHistoryResponseViewModel
{
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public DateTime TransDate { get; set; }
    public StockTransactionType StockTransactionType { get; set; }
    public int Quantity { get; set; }

}