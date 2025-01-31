using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.UpdateProduct.Command;
using InventoryManagementSystem.App.MappingProfiles;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.Products.UpdateProduct;

[Route("api/product/")]
public class UpdateProductEndpoint(BaseEndpointParam<UpdateProductRequestViewModel> param) : BaseEndpoint<UpdateProductRequestViewModel, bool>(param)
{
    [HttpPut]
    public async Task<EndpointResponse<bool>> UpdateProject([FromBody] UpdateProductRequestViewModel param)
    {
        var validateResult = ValidateRequest(param);

        if (!validateResult.IsSuccess)
            return validateResult;
        var command = param.Map<UpdateProductCommand>();

        var res = await _mediator.Send(command);

        return res.IsSuccess ? EndpointResponse<bool>.Success(res.Data, res.Message)
                             : EndpointResponse<bool>.Failure(res.ErrorCode, res.Message);
    }
}
