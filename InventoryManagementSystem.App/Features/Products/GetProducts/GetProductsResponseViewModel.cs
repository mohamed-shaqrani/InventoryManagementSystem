namespace InventoryManagementSystem.App.Features.Products.GetProducts;

public record GetProductsResponseViewModel(int Id, string Name, string Description, int Quantity, decimal Price, DateTime Date, int LowStockThreshold);
