using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.StockTransactions.DecreaseStockTrans.Command;
using InventoryManagementSystem.App.MappingProfiles;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.StockTransactions.DecreaseStockTrans;

[Route("api/stock-trans/Decrease")]
public class DecreaseStockTransEndpoint(BaseEndpointParam<DecreaseStockTransRequestViewModel> param)
    : BaseEndpoint<DecreaseStockTransRequestViewModel, bool>(param)
{
    [HttpPost]
    public async Task<EndpointResponse<bool>> DecreaseStockTrans([FromBody] DecreaseStockTransRequestViewModel param)
    {
        var validateResult = ValidateRequest(param);

        if (!validateResult.IsSuccess)
            return validateResult;
        var command = param.Map<DecreaseStockTransCommand>();

        var res = await _mediator.Send(command);
        return res.IsSuccess ? EndpointResponse<bool>.Success(res.Data, res.Message)
                             : EndpointResponse<bool>.Failure(res.ErrorCode, res.Message);
    }

}
