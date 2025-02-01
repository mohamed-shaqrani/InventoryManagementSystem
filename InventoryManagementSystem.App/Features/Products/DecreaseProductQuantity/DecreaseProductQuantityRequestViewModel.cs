using FluentValidation;

namespace InventoryManagementSystem.App.Features.Products.DecreaseProductQuantity;

public record DecreaseProductQuantityRequestViewModel(int Id, int Quantity);

public class DecreaseProductQuantityRequestViewModellValidator : AbstractValidator<DecreaseProductQuantityRequestViewModel>
{
    public DecreaseProductQuantityRequestViewModellValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}