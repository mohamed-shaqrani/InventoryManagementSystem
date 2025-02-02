using InventoryManagementSystem.App.Features.Auth.Registration.Command;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.Auth.Registration
{
    [Route("api/auth/register")]
    public class RegisterEndpoint : BaseEndpoint<RegisterViewModel, EndpointResponse<string>>
    {
        public RegisterEndpoint(BaseEndpointParam<RegisterViewModel> param) : base(param)
        {
        }

        [HttpPost]
        public async Task<EndpointResponse<string>> Register([FromForm] RegisterViewModel model)
        {
            var validationResult = ValidateRequest(model);
            if (!validationResult.IsSuccess)
            {
                return EndpointResponse<string>.Failure(validationResult.ErrorCode, validationResult.Message);
            }
            var register = new RegisterCommand(model.Username, model.Email, model.Password, model.phone, model.imageFile);

            var res = await _mediator.Send(register);

            if (res.IsSuccess)
            {
                return EndpointResponse<string>.Success(res.Data, res.Message);
            }

            return EndpointResponse<string>.Failure(res.ErrorCode, res.Message);
        }
    }
}
