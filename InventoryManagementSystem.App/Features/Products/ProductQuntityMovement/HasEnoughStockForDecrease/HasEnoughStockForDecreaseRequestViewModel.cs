using FluentValidation;

namespace InventoryManagementSystem.App.Features.Products.ProductQuntityMovement.HasEnoughStockForDecrease;

public record HasEnoughStockForDecreaseRequestViewModel(int Id, int Quantity);

public class HasEnoughStockForDecreaseRequestViewModelValidator : AbstractValidator<HasEnoughStockForDecreaseRequestViewModel>
{
    public HasEnoughStockForDecreaseRequestViewModelValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);

    }
}