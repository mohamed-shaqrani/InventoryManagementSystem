using FluentValidation;

namespace InventoryManagementSystem.App.Features.Products.AddProduct;

public record AddProductRequestViewModel(string Name, string Description, int Quantity, decimal Price, DateTime Date, int LowStockThreshold);

public class AddProductRequestViewModelValidator : AbstractValidator<AddProductRequestViewModel>
{
    public AddProductRequestViewModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.LowStockThreshold).GreaterThan(0);

    }
}