using FluentValidation;

namespace InventoryManagementSystem.App.Features.Products.GetProduct;

public record GetProductRequestViewModel(int Id);

public class GetProductRequestViewModelValidator : AbstractValidator<GetProductRequestViewModel>
{
    public GetProductRequestViewModelValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}