using InventoryManagementSystem.App.Entities;

namespace InventoryManagementSystem.App.Features.Products.Reports.LowStockReport;

public class LowStockReportResponseViewModel
{
    public string ProductName { get; set; }
    public int LowStockThreshold { get; set; }
    public int CurrentQuantity { get; set; }
    public int TotalIncreasedQuantity { get; set; } = 0;
    public int TotalDecreasedQuantity { get; set; } = 0;

    public DateTime TransDate { get; set; }
    public StockTransactionType StockTransactionType { get; set; }

}