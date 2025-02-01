using FluentValidation;
using InventoryManagementSystem.App.Entities;

namespace InventoryManagementSystem.App.Features.StockTransactions.AddStockTrans;

public record AddStockTransRequestViewModel(DateTime Date, StockTransactionType TransactionType, int UserId, int WarehouseId, List<StockTransDetailsViewModel> StockTransDetails);

public class AddStockTransRequestViewModelValidator : AbstractValidator<AddStockTransRequestViewModel>
{
    public AddStockTransRequestViewModelValidator()
    {
        RuleFor(RuleFor => RuleFor.Date).NotEmpty();
        RuleFor(RuleFor => RuleFor.TransactionType).IsInEnum();
        RuleFor(RuleFor => RuleFor.UserId).GreaterThan(0);
        RuleFor(RuleFor => RuleFor.WarehouseId).GreaterThan(0);
        RuleFor(RuleFor => RuleFor.StockTransDetails).NotEmpty();
        RuleForEach(RuleFor => RuleFor.StockTransDetails).SetValidator(new AddStockTransDetailsViewModelValidator());
    }
}
public record StockTransDetailsViewModel(int Quantity, int ProductId);
public class AddStockTransDetailsViewModelValidator : AbstractValidator<StockTransDetailsViewModel>
{
    public AddStockTransDetailsViewModelValidator()
    {
        RuleFor(RuleFor => RuleFor.Quantity).GreaterThan(0);
        RuleFor(RuleFor => RuleFor.ProductId).GreaterThan(0);
    }
}