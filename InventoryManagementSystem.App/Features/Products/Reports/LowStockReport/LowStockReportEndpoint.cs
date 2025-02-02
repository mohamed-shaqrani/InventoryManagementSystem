using InventoryManagementSystem.App.Extensions;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.Reports.LowStockReport.Query;
using InventoryManagementSystem.App.Helpers;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.Products.Reports.LowStockReport;
[Route("api/low-stock-report/")]
public class LowStockReportEndpoint(BaseEndpointParam<LowStockReportParam> param) : BaseEndpoint<LowStockReportParam, PageList<LowStockReportResponseViewModel>>(param)
{
    [HttpGet]
    public async Task<EndpointResponse<PageList<LowStockReportResponseViewModel>>> GetAllLowStockProducts([FromQuery] LowStockReportParam param)
    {


        var res = await _mediator.Send(new LowStockReportQuery(param));
        Response.AddPaginationHeader(res.Data);
        return res.IsSuccess ? EndpointResponse<PageList<LowStockReportResponseViewModel>>.Success(res.Data, res.Message)
                             : EndpointResponse<PageList<LowStockReportResponseViewModel>>.Failure(res.ErrorCode, res.Message);

    }
}
