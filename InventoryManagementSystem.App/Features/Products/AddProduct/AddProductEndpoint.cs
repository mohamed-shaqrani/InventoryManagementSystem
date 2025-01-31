using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.AddProduct.Command;
using InventoryManagementSystem.App.MappingProfiles;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.Products.AddProduct;

[Route("api/product/")]
public class AddProductEndpoint(BaseEndpointParam<AddProductRequestViewModel> param) : BaseEndpoint<AddProductRequestViewModel, bool>(param)
{
    [HttpPost]
    public async Task<EndpointResponse<bool>> AddProject([FromBody] AddProductRequestViewModel param)
    {
        var validateResult = ValidateRequest(param);

        if (!validateResult.IsSuccess)
            return validateResult;
        var command = param.Map<AddProductCommand>();

        var res = await _mediator.Send(command);

        return res.IsSuccess ? EndpointResponse<bool>.Success(res.Data, res.Message)
                             : EndpointResponse<bool>.Failure(res.ErrorCode, res.Message);
    }
}
