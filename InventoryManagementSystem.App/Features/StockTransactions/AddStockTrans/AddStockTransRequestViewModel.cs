using FluentValidation;
using InventoryManagementSystem.App.Entities;

namespace InventoryManagementSystem.App.Features.StockTransactions.AddStockTrans;

public record AddStockTransRequestViewModel(DateTime Date, StockTransactionType TransactionType, int UserId, int WarehouseId, int ProductId, int Quantity);

public class AddStockTransRequestViewModelValidator : AbstractValidator<AddStockTransRequestViewModel>
{
    public AddStockTransRequestViewModelValidator()
    {
        RuleFor(RuleFor => RuleFor.Date).NotEmpty();
        RuleFor(RuleFor => RuleFor.TransactionType).IsInEnum();
        RuleFor(RuleFor => RuleFor.UserId).GreaterThan(0);
        RuleFor(RuleFor => RuleFor.WarehouseId).GreaterThan(0);

    }
}
