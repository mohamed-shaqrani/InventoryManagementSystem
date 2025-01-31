using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.DeleteProduct.Command;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.Products.DeleteProduct;

[Route("api/product/")]
public class DeleteProductEndpoint(BaseEndpointParam<DeleteProductRequestViewModel> param)
    : BaseEndpoint<DeleteProductRequestViewModel, bool>(param)
{
    [HttpDelete("{id}")]
    public async Task<EndpointResponse<bool>> DeleteProject([FromRoute] int id)
    {
        var param = new DeleteProductRequestViewModel(id);
        var validateResult = ValidateRequest(param);

        if (!validateResult.IsSuccess)
            return validateResult;

        var res = await _mediator.Send(new DeleteProductCommand(param.Id));

        return res.IsSuccess ? EndpointResponse<bool>.Success(res.Data, res.Message)
                             : EndpointResponse<bool>.Failure(res.ErrorCode, res.Message);
    }
}
