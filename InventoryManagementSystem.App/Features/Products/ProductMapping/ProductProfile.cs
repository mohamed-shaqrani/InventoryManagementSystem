using AutoMapper;
using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Products.AddProduct;
using InventoryManagementSystem.App.Features.Products.AddProduct.Command;
using InventoryManagementSystem.App.Features.Products.GetProducts;

namespace InventoryManagementSystem.App.Features.Products.ProductMapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductRequestViewModel, AddProductCommand>();
        CreateMap<Product, GetProductsResponseViewModel>();
    }
}
