using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;

namespace InventoryManagementSystem.App.Features.Products.ProductQuntityMovement.HasEnoughStockForDecrease.Query;

public record HasEnoughStockForDecreaseQuery(int Id, int Quantity) : IRequest<RequestResult<bool>>;
public class HasEnoughStockForDecreaseHandler : BaseRequestHandler<HasEnoughStockForDecreaseQuery, RequestResult<bool>>
{
    private readonly IRepository<Product> _productRepository;

    public HasEnoughStockForDecreaseHandler(BaseRequestHandlerParam param, IRepository<Product> productRepository) : base(param)
    {
        _productRepository = productRepository;
    }
    public override async Task<RequestResult<bool>> Handle(HasEnoughStockForDecreaseQuery request, CancellationToken cancellationToken)
    {
        var hasEnoughStock = await _productRepository.AnyAsync(x => x.Id == request.Id && x.Quantity >= request.Quantity);

        if (!hasEnoughStock)
            return RequestResult<bool>.Failure(ErrorCode.InsufficientProductStock, "Insufficient Product Stock ");

        return RequestResult<bool>.Success(true, "Success");
    }
}
