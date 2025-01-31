using FluentValidation;

namespace InventoryManagementSystem.App.Features.Products.DeleteProduct;

public record DeleteProductRequestViewModel(int Id);

public class DeleteProductRequestViewModelValidator : AbstractValidator<DeleteProductRequestViewModel>
{
    public DeleteProductRequestViewModelValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}