using InventoryManagementSystem.App.Features.CapServices.ConsumeCapMsg;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.ProductQuntityMovement.HasEnoughStockForDecrease.Query;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InventoryManagementSystem.App.Features.StockTransactions.DecreaseStockTrans;

[Route("api/stock-trans/decrease")]
public class DecreaseStockTransEndpoint(BaseEndpointParam<DecreaseStockTransRequestViewModel> param) : BaseEndpoint<DecreaseStockTransRequestViewModel, bool>(param)
{
    [HttpPost]
    public async Task<EndpointResponse<bool>> DecreaseStockTrans([FromBody] DecreaseStockTransRequestViewModel param)
    {
        var validateResult = ValidateRequest(param);

        if (!validateResult.IsSuccess)
            return validateResult;
        var product = await _mediator.Send(new HasEnoughStockForDecreaseQuery(param.ProductId, param.Quantity));
        if (product.IsSuccess)
        {
            var decreseQuanityMsg = new DecreaseProductQtyMsg
            {
                ProductId = param.ProductId,
                Quantity = param.Quantity,
                Date = DateTime.UtcNow,
            };
            var message = JsonConvert.SerializeObject(decreseQuanityMsg);

            await _capPublisher.PublishAsync("decrease", message);
            //await _capPublisher.PublishAsync("captest", "decreseQuanityMsg");// works fine

        }
        //var orchViewModel = new ProductDecreaseOrchRequestViewModel(param.ProductId, param.Quantity);

        //var orchResult = await _mediator.Send(new ProductDecreaseOrchestrator(orchViewModel.Id, orchViewModel.Quantity));

        //if (!orchResult.IsSuccess)
        //{
        //    return EndpointResponse<bool>.Failure(orchResult.ErrorCode, orchResult.Message);
        //}

        //var command = param.Map<DecreaseStockTransCommand>();
        //var res = await _mediator.Send(command);

        //if (res.IsSuccess)
        //{
        //    var isQuantityBelowLowStock = await _mediator.Send(new IsQuantityBelowLowStockThresholdQuery(param.ProductId, param.Quantity));
        //    if (!isQuantityBelowLowStock.IsSuccess)
        //        await _rabbitMQPubService.PublishMessage(isQuantityBelowLowStock.Data);

        //}

        return EndpointResponse<bool>.Failure(ErrorCode.ChangePasswordError, "");
    }
}