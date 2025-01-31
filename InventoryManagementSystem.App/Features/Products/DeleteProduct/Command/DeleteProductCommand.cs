using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;

namespace InventoryManagementSystem.App.Features.Products.DeleteProduct.Command;

public record DeleteProductCommand(int Id) : IRequest<RequestResult<bool>>;
public class DeleteProductHandler : BaseRequestHandler<DeleteProductCommand, RequestResult<bool>>
{
    private readonly IRepository<Product> _productRepository;

    public DeleteProductHandler(BaseRequestHandlerParam param, IRepository<Product> productRepository) : base(param)
    {
        _productRepository = productRepository;
    }
    public override async Task<RequestResult<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var doesProductExist = await _productRepository.AnyAsync(p => p.Id == request.Id);

        if (!doesProductExist)
            return RequestResult<bool>.Failure(ErrorCode.ProductExists, "Product does not exist");


        var project = new Product
        {
            Id = request.Id,
            IsDeleted = true,

        };
        _productRepository.SaveInclude(project, a => a.IsDeleted);
        await _productRepository.SaveChangesAsync();

        return RequestResult<bool>.Success(true, "Success");
    }
}
