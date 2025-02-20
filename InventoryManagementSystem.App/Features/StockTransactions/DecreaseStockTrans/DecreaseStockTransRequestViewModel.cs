using FluentValidation;
using InventoryManagementSystem.App.Entities;

namespace InventoryManagementSystem.App.Features.StockTransactions.DecreaseStockTrans;

public record DecreaseStockTransRequestViewModel(DateTime Date, StockTransactionType TransactionType, int UserId, int WarehouseId, int Quantity, int ProductId);

public class DecreaseStockTransRequestViewModelValidator : AbstractValidator<DecreaseStockTransRequestViewModel>
{
    public DecreaseStockTransRequestViewModelValidator()
    {
        //RuleFor(RuleFor => RuleFor.Date).NotEmpty();
        //RuleFor(RuleFor => RuleFor.TransactionType).IsInEnum();
        //RuleFor(RuleFor => RuleFor.UserId).GreaterThan(0);
        //RuleFor(RuleFor => RuleFor.WarehouseId).GreaterThan(0);
        RuleFor(RuleFor => RuleFor.Quantity).GreaterThan(0);
        RuleFor(RuleFor => RuleFor.ProductId).GreaterThan(0);

    }
}
