using AutoMapper;
using InventoryManagementSystem.App.Features.Products.AddProduct;
using InventoryManagementSystem.App.Features.Products.AddProduct.Command;

namespace InventoryManagementSystem.App.Features.Products.ProductMapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductRequestViewModel, AddProductCommand>();
    }
}
