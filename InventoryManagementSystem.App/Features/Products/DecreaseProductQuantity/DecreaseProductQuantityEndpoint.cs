using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.DecreaseProductQuantity.Command;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.Products.DecreaseProductQuantity;

[Route("api/product/decrease-product-quantity")]
public class DecreaseProductQuantityEndpoint(BaseEndpointParam<DecreaseProductQuantityRequestViewModel> param) : BaseEndpoint<DecreaseProductQuantityRequestViewModel, bool>(param)
{
    [HttpPost]
    public async Task<EndpointResponse<bool>> DecreaseProductQuantity([FromBody] DecreaseProductQuantityRequestViewModel param)
    {
        var validateResult = ValidateRequest(param);

        if (!validateResult.IsSuccess)
            return validateResult;

        var res = await _mediator.Send(new DecreaseProductQuantityCommand(param.Id, param.Quantity));

        return res.IsSuccess ? EndpointResponse<bool>.Success(res.Data, res.Message)
                             : EndpointResponse<bool>.Failure(res.ErrorCode, res.Message);
    }
}
