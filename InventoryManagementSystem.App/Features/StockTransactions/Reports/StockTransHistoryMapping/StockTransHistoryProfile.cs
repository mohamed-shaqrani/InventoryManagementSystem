using AutoMapper;
using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.StockTransactions.Reports.StockTransHistory;

namespace InventoryManagementSystem.App.Features.StockTransactions.Reports.StockTransHistoryMapping;

public class StockTransHistoryProfile : Profile
{
    public StockTransHistoryProfile()
    {

        CreateMap<StockTransaction, GetStockTransHistoryResponseViewModel>()
                  .ForMember(des => des.ProductName, opt => opt.MapFrom(x => x.Product.Name))
                  .ForMember(des => des.ProductPrice, opt => opt.MapFrom(x => x.Product.Price)); ;
        ; ;
    }
}
