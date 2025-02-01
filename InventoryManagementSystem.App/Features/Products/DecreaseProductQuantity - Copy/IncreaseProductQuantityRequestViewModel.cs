using FluentValidation;

namespace InventoryManagementSystem.App.Features.Products.IncreaseProductQuantity;

public record IncreaseProductQuantityRequestViewModel(int Id, int Quantity);

public class IncreaseProductQuantityRequestViewModellValidator : AbstractValidator<IncreaseProductQuantityRequestViewModel>
{
    public IncreaseProductQuantityRequestViewModellValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}