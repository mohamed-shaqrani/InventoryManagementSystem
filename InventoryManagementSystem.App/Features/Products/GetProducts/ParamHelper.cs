﻿using FluentValidation;

namespace InventoryManagementSystem.App.Features.Products.GetProducts;

public class ProductParams
{
    private const int MaxPageSize = 30;
    private int _pageSize = 5;
    private int _pageNumber = 1;
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public string? SortBy { get; set; }

    public string? Title { get; set; }
}
public class GetProductsRequestViewModelValidator : AbstractValidator<ProductParams>
{
    public GetProductsRequestViewModelValidator()
    {

    }
}
