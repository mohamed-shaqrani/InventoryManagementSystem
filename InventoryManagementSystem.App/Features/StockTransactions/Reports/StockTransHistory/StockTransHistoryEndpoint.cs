using InventoryManagementSystem.App.Extensions;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.GetProducts;
using InventoryManagementSystem.App.Features.StockTransactions.Reports.StockTransHistory.Query;
using InventoryManagementSystem.App.Helpers;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.StockTransactions.Reports.StockTransHistory;
[Route("api/stock-trans-history/")]
public class StockTransHistoryEndpoint(BaseEndpointParam<ProductParams> param) : BaseEndpoint<ProductParams, PageList<GetStockTransHistoryResponseViewModel>>(param)
{
    [HttpGet]
    public async Task<EndpointResponse<PageList<GetStockTransHistoryResponseViewModel>>> GetProjects([FromQuery] StockTransHistoryParam param)
    {


        var res = await _mediator.Send(new StockTransHistoryQuery(param));
        Response.AddPaginationHeader(res.Data);
        return res.IsSuccess ? EndpointResponse<PageList<GetStockTransHistoryResponseViewModel>>.Success(res.Data, res.Message)
                             : EndpointResponse<PageList<GetStockTransHistoryResponseViewModel>>.Failure(res.ErrorCode, res.Message);

    }
}
