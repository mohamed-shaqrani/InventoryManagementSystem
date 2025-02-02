using FluentValidation;

namespace InventoryManagementSystem.App.Features.Products.GetProducts;

public class StockTransHistoryParam
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
    public string ProductName { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
public class StockTransHistoryParamViewModelValidator : AbstractValidator<StockTransHistoryParam>
{
    public StockTransHistoryParamViewModelValidator()
    {
        RuleFor(a => a).Must(HaveProductNameOrDate);


    }
    public bool HaveProductNameOrDate(StockTransHistoryParam param)
    {
        var res = !string.IsNullOrEmpty(param.ProductName) || param.FromDate.HasValue || param.ToDate.HasValue;
        return res;
    }
}
