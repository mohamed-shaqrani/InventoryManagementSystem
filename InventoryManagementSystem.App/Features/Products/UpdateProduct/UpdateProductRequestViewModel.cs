using FluentValidation;

namespace InventoryManagementSystem.App.Features.Products.UpdateProduct;

public record UpdateProductRequestViewModel(int Id, string Name, string Description, int Quantity, decimal Price, DateTime Date, int LowStockThreshold) : IProductBaseValidator;

public class UpdateProductRequestViewModelValidator : ProductBaseValidator<UpdateProductRequestViewModel>
{
    public UpdateProductRequestViewModelValidator()
    {

        RuleFor(x => x.Id).GreaterThan(0);
    }
}