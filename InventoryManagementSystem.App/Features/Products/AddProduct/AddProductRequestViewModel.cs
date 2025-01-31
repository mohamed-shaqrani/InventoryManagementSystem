namespace InventoryManagementSystem.App.Features.Products.AddProduct;

public record AddProductRequestViewModel(string Name, string Description, int Quantity, decimal Price, DateTime Date, int LowStockThreshold) : IProductBaseValidator;

public class AddProductRequestViewModelValidator : ProductBaseValidator<AddProductRequestViewModel>
{
    public AddProductRequestViewModelValidator()
    {

    }
}