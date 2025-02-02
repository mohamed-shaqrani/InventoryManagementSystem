using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Helpers;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;
using PredicateExtensions;
using System.Linq.Expressions;

namespace InventoryManagementSystem.App.Features.Products.Reports.LowStockReport.Query;

public record LowStockReportQuery(LowStockReportParam Params) : IRequest<RequestResult<PageList<LowStockReportResponseViewModel>>>;
public class LowStockReportHandler : BaseRequestHandler<LowStockReportQuery, RequestResult<PageList<LowStockReportResponseViewModel>>>
{

    private readonly IUnitOfWork _unitOfWork;

    public LowStockReportHandler(BaseRequestHandlerParam param, IUnitOfWork unitOfWork) : base(param)
    {

        _unitOfWork = unitOfWork;
    }
    public override async Task<RequestResult<PageList<LowStockReportResponseViewModel>>> Handle(LowStockReportQuery request, CancellationToken cancellationToken)
    {
        var predicate = BuildPredicate(request.Params);

        var query = from s in _unitOfWork.GetRepository<StockTransaction>().GetAll(predicate)
                    join p in _unitOfWork.GetRepository<Product>().GetAll() on s.ProductId equals p.Id
                    where p.LowStockThreshold > p.Quantity
                    group new { s, p } by new
                    {
                        p.Id,
                        p.Name,
                        p.Quantity,
                        p.LowStockThreshold,
                        s.Date,
                        s.TransactionType
                    } into g
                    orderby g.Key.Date descending
                    select new LowStockReportResponseViewModel
                    {
                        ProductName = g.Key.Name,
                        LowStockThreshold = g.Key.LowStockThreshold,
                        CurrentQuantity = g.Key.Quantity,
                        TransDate = g.Key.Date,
                        StockTransactionType = g.Key.TransactionType,
                        TotalDecreasedQuantity = g.Where(x => x.s.TransactionType == StockTransactionType.Decrease).Sum(x => x.s.Quantity),
                        TotalIncreasedQuantity = g.Where(x => x.s.TransactionType == StockTransactionType.Increase).Sum(x => x.s.Quantity)

                    };

        var paginatedList = await PageList<LowStockReportResponseViewModel>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize);

        return RequestResult<PageList<LowStockReportResponseViewModel>>.Success(paginatedList, "Success");
    }

    private Expression<Func<StockTransaction, bool>> BuildPredicate(LowStockReportParam request)
    {
        var predicate = PredicateExtensions.PredicateExtensions.Begin<StockTransaction>(true);
        if (request.CategoryId.HasValue)
            predicate = predicate.And(x => x.Product.CategoryId == request.CategoryId);

        if (request.StockTransactionType.HasValue)
            predicate = predicate.And(x => x.TransactionType == request.StockTransactionType.Value);

        if (request.FromDate.HasValue)
            predicate = predicate.And(x => x.Date >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            predicate = predicate.And(x => x.Date <= request.ToDate.Value);


        return predicate;
    }

}
