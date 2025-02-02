using FluentValidation;
using InventoryManagementSystem.App.Entities;

namespace InventoryManagementSystem.App.Features.Products.Reports.LowStockReport;

public class LowStockReportParam
{
    private const int MaxPageSize = 30;
    private int _pageSize = 5;
    private int _pageNumber = 1;
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
    public int? CategoryId { get; set; }

    public StockTransactionType? StockTransactionType { get; set; }
    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }
}
public class LowStockReportParamValidator : AbstractValidator<LowStockReportParam>
{
    public LowStockReportParamValidator()
    {


    }

}
