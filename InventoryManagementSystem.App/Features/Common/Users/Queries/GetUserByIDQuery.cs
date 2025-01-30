using MediatR;
using InventoryManagementSystem.App.Entities;
using InventoryManagementSystem.App.Repository;

namespace InventoryManagementSystem.App.Features.Common.Users.Queries
{
    public record GetUserByIDQuery(int userID) : IRequest<UserDTO>;

    public class GetUserByIDQueryHandler : BaseRequestHandler<GetUserByIDQuery, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIDQueryHandler(BaseRequestHandlerParam param, IUnitOfWork unitOfWork) : base(param)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<UserDTO> Handle(GetUserByIDQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(request.userID);

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Name = user.Username
            };
            return userDTO;

            throw new NotImplementedException();
        }
    }


}
