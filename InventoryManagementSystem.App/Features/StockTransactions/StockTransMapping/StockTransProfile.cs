using AutoMapper;
using InventoryManagementSystem.App.Features.StockTransactions.AddStockTrans;
using InventoryManagementSystem.App.Features.StockTransactions.AddStockTrans.Command;

namespace InventoryManagementSystem.App.Features.StockTransactions.StockTransMapping;

public class StockTransProfile : Profile
{
    public StockTransProfile()
    {
        CreateMap<AddStockTransCommand, AddStockTransRequestViewModel>().ReverseMap();


        CreateMap<StockTransDetailsViewModel, StockTransDetailsViewModel>().ReverseMap();

    }
}
