using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.GetProducts.Query;
using InventoryManagementSystem.App.Helpers;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.Products.GetProducts;
[Route("api/product/")]
public class GetProductsEndpoint(BaseEndpointParam<ProductParams> param) : BaseEndpoint<ProductParams, PageList<GetProductsResponseViewModel>>(param)
{
    [HttpGet]
    public async Task<EndpointResponse<PageList<GetProductsResponseViewModel>>> GetProjects([FromQuery] ProductParams param)
    {
        var validateResult = ValidateRequest(param);

        if (!validateResult.IsSuccess)
            return EndpointResponse<PageList<GetProductsResponseViewModel>>.Failure(validateResult.ErrorCode, validateResult.Message);

        var res = await _mediator.Send(new GetProductsQuery(param));
        return res.IsSuccess ? EndpointResponse<PageList<GetProductsResponseViewModel>>.Success(res.Data, res.Message)
                             : EndpointResponse<PageList<GetProductsResponseViewModel>>.Failure(res.ErrorCode, res.Message);

    }
}
