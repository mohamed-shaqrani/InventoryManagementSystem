using AutoMapper;
using InventoryManagementSystem.App.Features.StockTransactions.DecreaseStockTrans;
using InventoryManagementSystem.App.Features.StockTransactions.DecreaseStockTrans.Command;

namespace InventoryManagementSystem.App.Features.Products.ProductMapping;

public class StockMappingProfile : Profile
{
    public StockMappingProfile()
    {
        CreateMap<DecreaseStockTransCommand, DecreaseStockTransRequestViewModel>().ReverseMap();
    }
}
