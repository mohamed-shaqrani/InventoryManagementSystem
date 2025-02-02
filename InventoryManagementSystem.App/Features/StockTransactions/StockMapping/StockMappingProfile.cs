using AutoMapper;
using InventoryManagementSystem.App.Features.StockTransactions.AddStockTrans;
using InventoryManagementSystem.App.Features.StockTransactions.AddStockTrans.Command;
using InventoryManagementSystem.App.Features.StockTransactions.DecreaseStockTrans;
using InventoryManagementSystem.App.Features.StockTransactions.DecreaseStockTrans.Command;

namespace InventoryManagementSystem.App.Features.StockTransactions.StockMapping;

public class StockMappingProfile : Profile
{
    public StockMappingProfile()
    {
        CreateMap<DecreaseStockTransCommand, DecreaseStockTransRequestViewModel>().ReverseMap();
        CreateMap<AddStockTransCommand, AddStockTransRequestViewModel>().ReverseMap();

    }
}
