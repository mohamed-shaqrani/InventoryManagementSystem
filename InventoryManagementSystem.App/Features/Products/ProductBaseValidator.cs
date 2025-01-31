using FluentValidation;

namespace InventoryManagementSystem.App.Features.Products;


public interface IProductBaseValidator
{
    string Name { get; }
    string Description { get; }
    int Quantity { get; }
    decimal Price { get; }
    DateTime Date { get; }
    int LowStockThreshold { get; }
}


public class ProductBaseValidator<T> : AbstractValidator<T> where T : IProductBaseValidator
{
    protected ProductBaseValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.LowStockThreshold).GreaterThan(0);
    }
}
