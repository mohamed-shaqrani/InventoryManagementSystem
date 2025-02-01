//using InventoryManagementSystem.App.Entities;
//using InventoryManagementSystem.App.Features.Common;
//using InventoryManagementSystem.App.Repository;
//using InventoryManagementSystem.App.Response;
//using InventoryManagementSystem.App.Response.RequestResult;
//using MediatR;

//namespace InventoryManagementSystem.App.Features.StockTransactions.AddStockTrans.Command;

//public record StockTransOrchastrator(DateTime Date, StockTransactionType TransactionType, int UserId, int WarehouseId, List<StockTransDetailsViewModel> StockTransDetails) : IRequest<RequestResult<bool>>;
//public class StockTransHandler : BaseRequestHandler<AddStockTransCommand, RequestResult<bool>>
//{
//    private readonly IRepository<StockTransaction> _productRepository;

//    public StockTransHandler(BaseRequestHandlerParam param, IRepository<StockTransaction> productRepository) : base(param)
//    {
//        _productRepository = productRepository;
//    }
//    public override async Task<RequestResult<bool>> Handle(AddStockTransCommand request, CancellationToken cancellationToken)
//    {



//    }

//}
