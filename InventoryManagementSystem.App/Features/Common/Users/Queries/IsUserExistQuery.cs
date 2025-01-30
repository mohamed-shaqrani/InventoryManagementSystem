using MediatR;
using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Repository;

namespace InventoryManagementSystem.App.Features.Common.Users.Queries
{
    public record IsUserExistQuery(int userID) : IRequest<bool>;

    public class IsUserExistQueryHandler : IRequestHandler<IsUserExistQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public IsUserExistQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(IsUserExistQuery request, CancellationToken cancellationToken)
        {
            var isExist = await _unitOfWork.GetRepository<User>().AnyAsync(x => x.Id == request.userID);
            if (isExist)
            {
                return true;
            }
            return false;

        }
    }
}
