﻿using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.GetSingleProduct.Query;
using InventoryManagementSystem.App.Features.Products.HasEnoughStockForDecrease;
using InventoryManagementSystem.App.Features.Products.HasEnoughStockForDecrease.Query;
using InventoryManagementSystem.App.Features.Products.ProductDecreaseOrch;
using InventoryManagementSystem.App.Features.StockTransactions.DecreaseStockTrans.Command;
using InventoryManagementSystem.App.MappingProfiles;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.StockTransactions.DecreaseStockTrans;

[Route("api/stock-trans/decrease")]
public class DecreaseStockTransEndpoint(BaseEndpointParam<DecreaseStockTransRequestViewModel> param)
    : BaseEndpoint<DecreaseStockTransRequestViewModel, bool>(param)
{
    [HttpPost]
    public async Task<EndpointResponse<bool>> DecreaseStockTrans([FromBody] DecreaseStockTransRequestViewModel param)
    {
        var validateResult = ValidateRequest(param);

        if (!validateResult.IsSuccess)
            return validateResult;
        var orchViewModel = new ProductDecreaseOrchRequestViewModel (param.ProductId,param.Quantity);
        var orchResult =await _mediator.Send(new ProductDecreaseOrchestrator(orchViewModel));

        if (!orchResult.IsSuccess)
        {
            return  EndpointResponse<bool>.Failure(orchResult.ErrorCode, orchResult.Message);
        }
        var command = param.Map<DecreaseStockTransCommand>();

        var res = await _mediator.Send(command);

        if (res.IsSuccess)
        {
            var IsQuantityBelowLowStock = await _mediator.Send(new IsQuantityBelowLowStockThresholdQuery(param.ProductId, param.Quantity));
            if (IsQuantityBelowLowStock.IsSuccess)
            {
                EndpointResponse<bool>.Success(res.Data, res.Message);
            }
            //to do send message  to whom that concern
        }
        return    EndpointResponse<bool>.Failure(res.ErrorCode, res.Message);
    }

}
