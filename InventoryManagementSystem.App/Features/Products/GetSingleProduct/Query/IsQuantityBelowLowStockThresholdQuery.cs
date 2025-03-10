﻿using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.App.Features.Products.GetSingleProduct.Query;

public record IsQuantityBelowLowStockThresholdQuery(int Id, int Quantity) : IRequest<RequestResult<object>>;
public class IsQuantityBelowLowStockThresholdQueryHandler : BaseRequestHandler<IsQuantityBelowLowStockThresholdQuery, RequestResult<object>>
{
    private readonly IRepository<Product> _productRepository;

    public IsQuantityBelowLowStockThresholdQueryHandler(BaseRequestHandlerParam param, IRepository<Product> productRepository) : base(param)
    {
        _productRepository = productRepository;
    }
    public override async Task<RequestResult<object>> Handle(IsQuantityBelowLowStockThresholdQuery request, CancellationToken cancellationToken)
    {
        var IsQuantityBelowLowStock = await _productRepository.GetAll()
                                                              .Where(x => x.Id == request.Id && x.Quantity < request.Quantity)
                                                              .Select(x => new
                                                              {
                                                                  x.Id,
                                                                  x.Name,
                                                              }).FirstOrDefaultAsync();

        if (IsQuantityBelowLowStock != null)
        {
            var data = new
            {
                Message = $"Warning: product {IsQuantityBelowLowStock.Name} with Id {IsQuantityBelowLowStock.Id} is Below Low Stock",
                DateAndTime = DateTime.UtcNow
            };

            return RequestResult<object>.Failure(ErrorCode.QuantityBelowLowStockWarning, data.Message, data);
        }

        return RequestResult<object>.Success(false, "Success");
    }
}
