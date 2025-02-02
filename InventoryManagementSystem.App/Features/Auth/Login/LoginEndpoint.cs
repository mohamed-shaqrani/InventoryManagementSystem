using InventoryManagementSystem.App.Features.Auth.Login;
using InventoryManagementSystem.App.Features.Auth.Login.Command;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.Authentication.Login
{
    [Route("api/auth/login")]
    public class LoginEndpoint : BaseEndpoint<LoginViewModel, LoginViewModel>
    {
        public LoginEndpoint(BaseEndpointParam<LoginViewModel> param) : base(param)
        {

        }


        [HttpPost]

        public async Task<EndpointResponse<AuthModel>> Login([FromBody] LoginViewModel model)
        {
            var validationResult = ValidateRequest(model);
            if (!validationResult.IsSuccess)
                return EndpointResponse<AuthModel>.Failure(validationResult.ErrorCode, validationResult.Message);

            var logincommand = new LoginCommand(model.Email, model.Password);

            var res = await _mediator.Send(logincommand);

            return res.IsSuccess ? EndpointResponse<AuthModel>.Success(res.Data, res.Message)
                                 : EndpointResponse<AuthModel>.Failure(res.ErrorCode, res.Message);
        }
    }
}
