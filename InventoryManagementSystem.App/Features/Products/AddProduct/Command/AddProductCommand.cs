using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;

namespace InventoryManagementSystem.App.Features.Products.AddProduct.Command;

public record AddProductCommand(string Name, string Description, int Quantity, decimal Price, DateTime Date, int LowStockThreshold) : IRequest<RequestResult<bool>>;
public class AddProductHandler : BaseRequestHandler<AddProductCommand, RequestResult<bool>>
{
    private readonly IRepository<Product> _productRepository;

    public AddProductHandler(BaseRequestHandlerParam param, IRepository<Product> productRepository) : base(param)
    {
        _productRepository = productRepository;
    }
    public override async Task<RequestResult<bool>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var doesProductExist = await _productRepository.AnyAsync(p => p.Name == request.Name);

        if (doesProductExist)
            return RequestResult<bool>.Failure(ErrorCode.ProductExists, "Product already exists");


        var project = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Quantity = request.Quantity,
            Price = request.Price,
            Date = request.Date,
            LowStockThreshold = 10,
        };
        await _productRepository.AddAsync(project);
        await _productRepository.SaveChangesAsync();

        return RequestResult<bool>.Success(true, "Success");
    }
}
