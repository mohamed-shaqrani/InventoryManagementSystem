using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.ProductQuntityMovement.HasEnoughStockForDecrease.Query;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.Products.ProductQuntityMovement.HasEnoughStockForDecrease;

[Route("api/product/has-enough-stock-decrease")]
public class HasEnoughStockForDecreaseEndpoint(BaseEndpointParam<HasEnoughStockForDecreaseRequestViewModel> param)
    : BaseEndpoint<HasEnoughStockForDecreaseRequestViewModel, bool>(param)
{
    [HttpGet("{id}/{quantity}")]
    public async Task<EndpointResponse<bool>> HasEnoughStockForDecrease([FromRoute] int id, [FromRoute] int quantity)
    {
        var param = new HasEnoughStockForDecreaseRequestViewModel(id, quantity);
        var validateResult = ValidateRequest(param);

        if (!validateResult.IsSuccess)
            return validateResult;

        var res = await _mediator.Send(new HasEnoughStockForDecreaseQuery(param.Id, param.Quantity));

        return res.IsSuccess ? EndpointResponse<bool>.Success(res.Data, res.Message)
                             : EndpointResponse<bool>.Failure(res.ErrorCode, res.Message);
    }
}
