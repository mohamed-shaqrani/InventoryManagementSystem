using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Products.GetProducts;
using InventoryManagementSystem.App.Helpers;
using InventoryManagementSystem.App.MappingProfiles;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;
using PredicateExtensions;
using System.Linq.Expressions;

namespace InventoryManagementSystem.App.Features.StockTransactions.Reports.StockTransHistory.Query;

public record StockTransHistoryQuery(StockTransHistoryParam Params) : IRequest<RequestResult<PageList<GetStockTransHistoryResponseViewModel>>>;
public class StockTransHistoryHandler : BaseRequestHandler<StockTransHistoryQuery, RequestResult<PageList<GetStockTransHistoryResponseViewModel>>>
{
    private readonly IRepository<StockTransaction> _stockTransRepository;

    public StockTransHistoryHandler(BaseRequestHandlerParam param, IRepository<StockTransaction> stockTransRepository) : base(param)
    {
        _stockTransRepository = stockTransRepository;
    }
    public override async Task<RequestResult<PageList<GetStockTransHistoryResponseViewModel>>> Handle(StockTransHistoryQuery request, CancellationToken cancellationToken)
    {
        var prdicate = BuildPredicate(request);
        var query = _stockTransRepository.GetAll(prdicate).ProjectTo<GetStockTransHistoryResponseViewModel>();
        var paginatedList = await PageList<GetStockTransHistoryResponseViewModel>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize);
        return RequestResult<PageList<GetStockTransHistoryResponseViewModel>>.Success(paginatedList, "Success");
    }
    private Expression<Func<StockTransaction, bool>> BuildPredicate(StockTransHistoryQuery request)
    {
        var predicate = PredicateExtensions.PredicateExtensions.Begin<StockTransaction>(true);
        predicate = predicate.And(x => x.Product.Name.Contains(request.Params.ProductName));

        predicate = predicate.And(x => x.Date >= request.Params.FromDate);


        predicate = predicate.And(x => x.Date <= request.Params.ToDate);


        return predicate;
    }

}
