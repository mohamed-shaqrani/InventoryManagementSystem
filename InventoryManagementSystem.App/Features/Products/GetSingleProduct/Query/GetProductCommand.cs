using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.GetProducts;
using InventoryManagementSystem.App.MappingProfiles;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;

namespace InventoryManagementSystem.App.Features.Products.GetSingleProduct.Query;

public record GetProductCommand(int Id) : IRequest<RequestResult<GetProductsResponseViewModel>>;
public class GetProductHandler : BaseRequestHandler<GetProductCommand, RequestResult<GetProductsResponseViewModel>>
{
    private readonly IRepository<Product> _productRepository;

    public GetProductHandler(BaseRequestHandlerParam param, IRepository<Product> productRepository) : base(param)
    {
        _productRepository = productRepository;
    }
    public override async Task<RequestResult<GetProductsResponseViewModel>> Handle(GetProductCommand request, CancellationToken cancellationToken)
    {
        var singleProduct = await _productRepository.GetByIdAsync(request.Id);

        if (singleProduct is null)
            return RequestResult<GetProductsResponseViewModel>.Failure(ErrorCode.ProductDoesNotExist, "Product does not exist");

        var res = singleProduct.Map<GetProductsResponseViewModel>();

        return RequestResult<GetProductsResponseViewModel>.Success(res, "Success");
    }
}
