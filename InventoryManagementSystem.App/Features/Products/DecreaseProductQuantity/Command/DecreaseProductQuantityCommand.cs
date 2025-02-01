using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Repository;
using InventoryManagementSystem.App.Response;
using InventoryManagementSystem.App.Response.RequestResult;
using MediatR;

namespace InventoryManagementSystem.App.Features.Products.DecreaseProductQuantity.Command;

public record DecreaseProductQuantityCommand(int Id, int Quantity) : IRequest<RequestResult<bool>>;
public class DecreaseProductQuantityHandler : BaseRequestHandler<DecreaseProductQuantityCommand, RequestResult<bool>>
{
    private readonly IRepository<Product> _productRepository;

    public DecreaseProductQuantityHandler(BaseRequestHandlerParam param, IRepository<Product> productRepository) : base(param)
    {
        _productRepository = productRepository;
    }
    public override async Task<RequestResult<bool>> Handle(DecreaseProductQuantityCommand request, CancellationToken cancellationToken)
    {

        var currentQuantity = _productRepository.GetAll().Where(a=>a.Id ==request.Id).Select(a=>a.Quantity).First();
        var newQuantity = currentQuantity - request.Quantity;
        var product = new Product
        {
            Id = request.Id,
            Quantity = newQuantity,

        };
        _productRepository.SaveInclude(product, x => x.Quantity);
        var result = await _productRepository.SaveChangesAsync();

        return result > 0 ? RequestResult<bool>.Success(true, "Success")
                          : RequestResult<bool>.Failure(ErrorCode.DataBaseError, "Something went wrong");


    }
}
