using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;

namespace InventoryManagementSystem.App.Features.Products.UpdateProduct.Command;

public sealed record UpdateProductCommand(int Id,string Name, string Description, int Quantity, decimal Price, int LowStockThreshold) : IRequest<RequestResult<bool>>;
public sealed class UpdateProductHandler : BaseRequestHandler<UpdateProductCommand, RequestResult<bool>>
{
    private readonly IRepository<Product> _productRepository;

    public UpdateProductHandler(BaseRequestHandlerParam param, IRepository<Product> productRepository) : base(param)
    {
        _productRepository = productRepository;
    }
    public override async Task<RequestResult<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var doesProductExist = await _productRepository.AnyAsync(p => p.Name == request.Name);

        if (doesProductExist)
            return RequestResult<bool>.Failure(ErrorCode.ProductExists, "Product already exists");


        var project = new Product
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Quantity = request.Quantity,
            Price = request.Price,
            LowStockThreshold = 10,
        };
         _productRepository.SaveInclude(project,a=>a.Price,a=>a.Name,a=>a.Description,a=>a.LowStockThreshold);
        await _productRepository.SaveChangesAsync();

        return RequestResult<bool>.Success(true, "project has been updated Successfully");
    }
}
