﻿using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.StockTransactions.AddStockTrans.Command;
using InventoryManagementSystem.App.MappingProfiles;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.StockTransactions.AddStockTrans;

[Route("api/stock-trans/add")]
public class AddStockTransEndpoint(BaseEndpointParam<AddStockTransRequestViewModel> param)
    : BaseEndpoint<AddStockTransRequestViewModel, bool>(param)
{
    [HttpPost]
    public async Task<EndpointResponse<bool>> AddStockTrans([FromBody] AddStockTransRequestViewModel param)
    {
        var validateResult = ValidateRequest(param);

        if (!validateResult.IsSuccess)
            return validateResult;
        var command = param.Map<AddStockTransCommand>();

        var res = await _mediator.Send(command);
        return res.IsSuccess ? EndpointResponse<bool>.Success(res.Data, res.Message)
                             : EndpointResponse<bool>.Failure(res.ErrorCode, res.Message);
    }

}
