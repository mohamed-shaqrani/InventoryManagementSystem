using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.GetProduct;
using InventoryManagementSystem.App.Features.Products.GetProducts;
using InventoryManagementSystem.App.Features.Products.GetSingleProduct.Query;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.Products.GetSingleProduct;

[Route("api/product/")]
public class GetProductEndpoint(BaseEndpointParam<GetProductRequestViewModel> param)
    : BaseEndpoint<GetProductRequestViewModel, GetProductsResponseViewModel>(param)
{
    [HttpGet("{id}")]
    public async Task<EndpointResponse<GetProductsResponseViewModel>> GetSingleProduct([FromRoute] int id)
    {
        var param = new GetProductRequestViewModel(id);
        var validateResult = ValidateRequest(param);

        if (!validateResult.IsSuccess)
            return validateResult;

        var res = await _mediator.Send(new GetSingleProductQuery(param.Id));

        return res.IsSuccess ? EndpointResponse<GetProductsResponseViewModel>.Success(res.Data, res.Message)
                             : EndpointResponse<GetProductsResponseViewModel>.Failure(res.ErrorCode, res.Message);
    }
}
