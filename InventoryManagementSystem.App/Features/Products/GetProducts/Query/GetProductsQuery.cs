using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Helpers;
using InventoryManagementSystem.App.MappingProfiles;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;

namespace InventoryManagementSystem.App.Features.Products.GetProducts.Query;

public record GetProductsQuery(ProductParams Params) : IRequest<RequestResult<PageList<GetProductsResponseViewModel>>>;
public class GetProductsHandler : BaseRequestHandler<GetProductsQuery, RequestResult<PageList<GetProductsResponseViewModel>>>
{
    private readonly IRepository<Product> _productRepository;

    public GetProductsHandler(BaseRequestHandlerParam param, IRepository<Product> productRepository) : base(param)
    {
        _productRepository = productRepository;
    }
    public override async Task<RequestResult<PageList<GetProductsResponseViewModel>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _productRepository.GetAll().ProjectTo<GetProductsResponseViewModel>();
        var paginatedList = await PageList<GetProductsResponseViewModel>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize);
        return RequestResult<PageList<GetProductsResponseViewModel>>.Success(paginatedList, "Success");
    }
}
