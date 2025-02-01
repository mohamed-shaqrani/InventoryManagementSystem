using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.IncreaseProductQuantity.Command;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.Products.IncreaseProductQuantity;

[Route("api/product/increase-product-quantity")]
public class IncreaseProductQuantityEndpoint(BaseEndpointParam<IncreaseProductQuantityRequestViewModel> param) : BaseEndpoint<IncreaseProductQuantityRequestViewModel, bool>(param)
{
    [HttpPost]
    public async Task<EndpointResponse<bool>> IncreaseProductQuantity([FromBody] IncreaseProductQuantityRequestViewModel param)
    {
        var validateResult = ValidateRequest(param);

        if (!validateResult.IsSuccess)
            return validateResult;

        var res = await _mediator.Send(new IncreaseProductQuantityCommand(param.Id, param.Quantity));

        return res.IsSuccess ? EndpointResponse<bool>.Success(res.Data, res.Message)
                             : EndpointResponse<bool>.Failure(res.ErrorCode, res.Message);
    }
}
