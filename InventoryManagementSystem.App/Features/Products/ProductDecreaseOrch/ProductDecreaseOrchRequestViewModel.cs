using FluentValidation;

namespace InventoryManagementSystem.App.Features.Products.HasEnoughStockForDecrease;

public record ProductDecreaseOrchRequestViewModel(int Id, int Quantity);

public class ProductDecreaseOrchRequestViewModelValidator : AbstractValidator<ProductDecreaseOrchRequestViewModel>
{
    public ProductDecreaseOrchRequestViewModelValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);

    }
}